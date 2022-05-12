using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SurveyMe.Common.Microsoft.Logging;
using SurveyMe.Data;
using SurveyMe.Data.Stores;
using SurveyMe.DomainModels;
using SurveyMe.Foundation.Models;
using SurveyMe.Common.Time;
using SurveyMe.Foundation.MapperConfigurations.Profiles;
using SurveyMe.Foundation.Services;
using SurveyMe.Foundation.Services.Abstracts;
using SurveyMe.WebApplication.Extensions;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logBuilder =>
{
    logBuilder.AddLogger();
    logBuilder.AddFile(builder.Configuration.GetSection("Serilog:FileLogging"));
});

var version = builder.Configuration
    .GetSection("SwaggerConfiguration:ApiVersion").Value;

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc($"api-v{version}", new OpenApiInfo
    {
        Title = "SurveyMe Api",
        Version = version
    });

    var filePath = Path.Combine(AppContext.BaseDirectory, "SurveyMe.WebApplication.xml");
    options.IncludeXmlComments(filePath);
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<FileServiceConfiguration>(builder.Configuration.GetSection("FileService"));

builder.Services.AddDbContext<SurveyMeDbContext>(options
    => options.UseSqlServer(builder.Configuration
        .GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISurveyMeUnitOfWork, SurveyMeUnitOfWork>();

builder.Services.AddAutoMapper(configuration =>
{
    configuration.AddMaps(typeof(Program).Assembly);
    configuration.AddProfile<SurveyStatisticProfile>();
});


builder.Services.AddIdentity<User,Role>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 8;
    })
    .AddRoleStore<RoleStore>()
    .AddUserStore<UserStore>();

/*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = false
        };
    });*/

builder.Services.AddScoped<ISurveyMeUnitOfWork, SurveyMeUnitOfWork>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddScoped<ISurveyAnswersService, SurveySurveyAnswersService>();
builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddSingleton<ISystemClock, SystemClock>();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
builder.Services.AddSingleton<ITokenGenerator, TokenGenerator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/api-v{version}/swagger.json", $"api-v{version}");
    });
    app.UseCustomExceptionHandler();
}

await app.Services.CreateDbIfNotExists();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();