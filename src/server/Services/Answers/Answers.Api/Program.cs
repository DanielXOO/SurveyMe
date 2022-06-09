using System.Text.Json.Serialization;
using Answers.Api.Converters;
using Answers.Api.Extensions;
using Answers.Data;
using Answers.Data.Abstracts;
using Answers.Services;
using Answers.Services.Abstracts;
using IdentityServer4.AccessTokenValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AnswersDbContext>(options
    => options.UseSqlServer(builder.Configuration
        .GetConnectionString("DefaultConnection")));

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new AnswerJsonConverter());
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddAutoMapper(configuration =>
{
    configuration.AddMaps(typeof(Program).Assembly);
});

builder.Services.AddScoped<IAnswersService, AnswersService>();
builder.Services.AddScoped<IAnswersUnitOfWork, AnswersUnitOfWork>();

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