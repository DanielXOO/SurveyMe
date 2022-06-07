using Authentication.Api.Common.Time;
using Authentication.Api.Configurations;
using Authentication.Api.Data;
using Authentication.Api.Data.Abstracts;
using Authentication.Api.Data.Stores;
using Authentication.Api.Extensions;
using Authentication.Api.Logging;
using Authentication.Api.Models.Roles;
using Authentication.Api.Models.Users;
using Authentication.Api.Services;
using Authentication.Api.Services.Abstracts;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logBuilder =>
{
    logBuilder.AddLogger();
    logBuilder.AddFile(builder.Configuration.GetSection("Serilog:FileLogging"));
});

builder.Services.AddControllers();

builder.Services.AddDbContext<AuthenticationDbContext>(options
    => options.UseSqlServer(builder.Configuration
        .GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAuthenticationUnitOfWork, AuthenticationUnitOfWork>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddSingleton<ISystemClock, SystemClock>();

builder.Services.AddAutoMapper(configuration =>
{
    configuration.AddMaps(typeof(Program).Assembly);
});


builder.Services.AddIdentityCore<User>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 8;
    })
    .AddSignInManager()
    .AddUserStore<UserStore>()
    .AddRoles<Role>()
    .AddRoleStore<RoleStore>();

builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(Configurations.Resources)
    .AddInMemoryClients(Configurations.Clients)
    .AddInMemoryApiResources(Configurations.ApiResources)
    .AddInMemoryApiScopes(Configurations.ApiScopes)
    .AddDeveloperSigningCredential()
    .AddAspNetIdentity<User>();

builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
    .AddIdentityServerAuthentication(options =>
    {
        options.Authority = "https://localhost:7179";
    });

var app = builder.Build();

await app.Services.CreateDbIfNotExists();

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseRouting();

app.UseIdentityServer();

app.MapControllers();

app.Run();