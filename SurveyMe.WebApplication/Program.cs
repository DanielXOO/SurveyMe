using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SurveyMe.Common.Microsoft.Logging;
using SurveyMe.Repositories;
using SurveyMe.WebApplication;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logBuilder =>
{
    logBuilder.AddLogger();
    logBuilder.AddFile(builder.Configuration.GetSection("Serilog:FileLogging"));
});

var version = builder.Configuration
    .GetSection("ApplicationInfo:Version").Value;

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    var applicationVersion = builder.Configuration
        .GetSection("ApplicationInfo:Version").Value;

    options.SwaggerDoc($"api-v{applicationVersion}", new OpenApiInfo
    {
        Title = "SurveyMe Api",
        Version = applicationVersion
    });
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<SurveyMeDbContext>(options 
    => options.UseSqlServer(builder.Configuration
        .GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/api-v{version}/swagger.json", $"api-v{version}");
    });
}

app.Services.CreateDbIfNotExists();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();