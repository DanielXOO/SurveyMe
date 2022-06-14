using System.Text.Json.Serialization;
using IdentityServer4.AccessTokenValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SurveyMe.Common.Logging;
using SurveyMe.Common.Time;
using Surveys.Api.Extensions;
using Surveys.Data;
using Surveys.Data.Abstracts;
using Surveys.Services;
using Surveys.Services.Abstracts;
using Surveys.Services.Automapper.Profiles;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logBuilder =>
{
    logBuilder.AddLogger();
    logBuilder.AddFile(builder.Configuration.GetSection("Serilog:FileLogging"));
});

builder.Services.AddDbContext<SurveysDbContext>(options
    => options.UseSqlServer(builder.Configuration
        .GetConnectionString("DefaultConnection")));

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddAutoMapper(configuration =>
{
    configuration.AddMaps(typeof(Program).Assembly);
    configuration.AddProfile(new QueueModelsProfile());
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.Services.CreateDbIfNotExists();

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();