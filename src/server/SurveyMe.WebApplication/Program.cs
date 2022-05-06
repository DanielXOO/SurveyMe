using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SurveyMe.Common.Microsoft.Logging;
using SurveyMe.Data;
using SurveyMe.Data.Stores;
using SurveyMe.DomainModels;
using SurveyMe.WebApplication;
using SurveyMe.Foundation.Models;
using SurveyMe.Foundation.Services.Account;
using SurveyMe.Foundation.Services.Answers;
using SurveyMe.Foundation.Services.Files;
using SurveyMe.Foundation.Services.Surveys;
using SurveyMe.Foundation.Services.Users;
using SurveyMe.Common.Time;
using SurveyMe.Foundation.Services.Abstracts;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logBuilder =>
{
    logBuilder.AddLogger();
    logBuilder.AddFile(builder.Configuration.GetSection("Serilog:FileLogging"));
});

var version = builder.Configuration
    .GetSection("ApplicationInfo:Version").Value;

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc($"api-v{version}", new OpenApiInfo
    {
        Title = "SurveyMe Api",
        Version = version
    });
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<FileServiceConfiguration>(builder.Configuration.GetSection("FileService"));

builder.Services.AddDbContext<SurveyMeDbContext>(options
    => options.UseSqlServer(builder.Configuration
        .GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISurveyMeUnitOfWork, SurveyMeUnitOfWork>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddIdentity<User, Role>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 8;
    })
    .AddRoleStore<RoleStore>()
    .AddUserStore<UserStore>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<ISurveyMeUnitOfWork, SurveyMeUnitOfWork>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddScoped<ISurveyAnswersService, SurveySurveyAnswersService>();
builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddSingleton<ISystemClock, SystemClock>();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/api-v{version}/swagger.json", $"api-v{version}");
    });
}

await app.Services.CreateDbIfNotExists();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();