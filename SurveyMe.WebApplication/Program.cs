using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var version = builder.Configuration
    .GetSection("ApplicationInfo:Version").Value;

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    var ApplicationVersion = builder.Configuration
        .GetSection("ApplicationInfo:Version").Value;

    options.SwaggerDoc($"api-v{ApplicationVersion}", new OpenApiInfo
    {
        Title = "SurveyMe Api",
        Version = ApplicationVersion
    });
});
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/api-v{version}/swagger.json", $"api-v{version}");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();