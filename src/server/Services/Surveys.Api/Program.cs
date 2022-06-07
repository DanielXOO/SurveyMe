using System.Text.Json.Serialization;
using IdentityServer4.AccessTokenValidation;
using Microsoft.EntityFrameworkCore;
using Surveys.Api.Common.Time;
using Surveys.Api.Data;
using Surveys.Api.Data.Abstracts;
using Surveys.Api.Extensions;
using Surveys.Api.Services;
using Surveys.Api.Services.Abstracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SurveysDbContext>(options
    => options.UseSqlServer(builder.Configuration
        .GetConnectionString("DefaultConnection")));

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddAutoMapper(configuration =>
{
    configuration.AddMaps(typeof(Program).Assembly);
});

builder.Services.AddScoped<ISurveysService, SurveysService>();
builder.Services.AddScoped<ISurveysUnitOfWork, SurveysUnitOfWork>();

builder.Services.AddSingleton<ISystemClock, SystemClock>();

builder.Services.AddControllers();

builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
    .AddIdentityServerAuthentication(options =>
    {
        options.Authority = "https://localhost:7179";
        options.RequireHttpsMetadata = false;
        options.ApiName = "SurveyMeApi";
        options.ApiSecret = "api_secret";
        options.JwtValidationClockSkew = TimeSpan.FromSeconds(1);
    });

var app = builder.Build();

await app.Services.CreateDbIfNotExists();

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();