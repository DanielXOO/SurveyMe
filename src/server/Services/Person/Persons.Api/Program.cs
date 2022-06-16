using IdentityServer4.AccessTokenValidation;
using Person.Api.Extensions;
using Persons.Data;
using Persons.Data.Repositories;
using Persons.Data.Repositories.Abstracts;
using Persons.Models.Configurations;
using Persons.Services;
using Persons.Services.Abstracts;
using SurveyMe.Common.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logBuilder =>
{
    logBuilder.AddLogger();
    logBuilder.AddFile(builder.Configuration.GetSection("Serilog:FileLogging"));
});

builder.Services.AddSwaggerGen();

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<PersonsDbContext>();
builder.Services.AddScoped<IPersonalityRepository, PersonalityRepository>();
builder.Services.AddScoped<IPersonalityService, PersonalityService>();

builder.Services.Configure<DbConfiguration>(builder.Configuration.GetSection("DbConfiguration"));

builder.Services.AddAutoMapper(configurations =>
{
    configurations.AddMaps(typeof(Program).Assembly);
});

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

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();