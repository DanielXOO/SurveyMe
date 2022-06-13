using System.Text.Json.Serialization;
using Answers.Api.Converters;
using Answers.Api.Extensions;
using Answers.Data;
using Answers.Data.Abstracts;
using Answers.Services;
using Answers.Services.Abstracts;
using Answers.Services.Consumers;
using IdentityServer4.AccessTokenValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SurveyMe.Common.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logBuilder =>
{
    logBuilder.AddLogger();
    logBuilder.AddFile(builder.Configuration.GetSection("Serilog:FileLogging"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AnswersDbContext>(options
    => options.UseSqlServer(builder.Configuration
        .GetConnectionString("DefaultConnection")));

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new AnswerJsonConverter());
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddAutoMapper(configuration =>
{
    configuration.AddMaps(typeof(Program).Assembly);
});

builder.Services.AddScoped<IAnswersService, AnswersService>();
builder.Services.AddScoped<ISurveysService, SurveysService>();
builder.Services.AddScoped<IAnswersUnitOfWork, AnswersUnitOfWork>();

builder.Services.AddControllers();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SurveysConsumer>();
    x.UsingRabbitMq((context, config) =>
    {
        config.Host("localhost", "/", configuration =>
        {
            configuration.Username("guest");
            configuration.Password("guest");
        });
        config.ConfigureEndpoints(context);
    });
});

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