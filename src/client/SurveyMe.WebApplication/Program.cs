using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Refit;
using SurveyMe.Data.Abstracts;
using SurveyMe.Services;
using SurveyMe.Services.Abstracts;
using SurveyMe.WebApplication.Converters;
using SurveyMe.WebApplication.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.Converters.Add(new AnswerViewJsonConverter());
});

var baseApiAddress = new Uri(builder.Configuration["ApiConfiguration:BaseAddress"]);

builder.Services.AddRefitClient<IAccountApi>()
    .ConfigureHttpClient(configuration =>
    {
        configuration.BaseAddress = baseApiAddress;
    });

builder.Services.AddRefitClient<IUserApi>()
    .ConfigureHttpClient(configuration =>
    {
        configuration.BaseAddress = baseApiAddress;
    });

builder.Services.AddRefitClient<IFileApi>()
    .ConfigureHttpClient(configuration =>
    {
        configuration.BaseAddress = baseApiAddress;
    });

builder.Services.AddRefitClient<ISurveyApi>(new RefitSettings()
    {
        ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
        {
            Converters = { new AnswerRequestJsonConverter(), new JsonStringEnumConverter()},
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        })
    })
    .ConfigureHttpClient(configuration =>
    {
        configuration.BaseAddress = baseApiAddress;
    });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISurveyService, SurveyService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}
app.UseCustomExceptionHandler();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        "default",
        "{controller=Users}/{action=Index}/{id?}");
});

app.Run();