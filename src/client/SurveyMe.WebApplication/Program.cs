using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.StaticFiles;
using Refit;
using SurveyMe.Common.Microsoft.Logging;
using SurveyMe.Data.Abstracts;
using SurveyMe.DomainModels.Request.Authentication;
using SurveyMe.Services;
using SurveyMe.Services.Abstracts;
using SurveyMe.WebApplication.Converters;
using SurveyMe.WebApplication.Extensions;
using SurveyMe.WebApplication.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logBuilder =>
{
    logBuilder.AddLogger();
    logBuilder.AddFile(builder.Configuration.GetSection("Serilog:FileLogging"));
});

builder.Services.Configure<IdentityServerConfiguration>(builder.Configuration.GetSection("IdentityServer"));

builder.Services.AddMvc()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.Converters.Add(new AnswerViewJsonConverter());
});

builder.Services.AddHttpContextAccessor();

var baseApiAddress = new Uri(builder.Configuration["ApiConfiguration:BaseAddress"]);

builder.Services.AddRefitClient<IAccountApi>()
    .ConfigureHttpClient(configuration =>
    {
        configuration.BaseAddress = new Uri("https://localhost:7179");
    })
    .AddHttpMessageHandler<SignInHandler>();

builder.Services.AddRefitClient<IUserApi>()
    .ConfigureHttpClient(configuration =>
    {
        configuration.BaseAddress = baseApiAddress;
    })
    .AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddRefitClient<IAnswersApi>(new RefitSettings()
{
    ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
    {
        Converters = { new AnswerRequestJsonConverter(), new JsonStringEnumConverter() },
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    })
})
    .ConfigureHttpClient(configuration =>
    {
        configuration.BaseAddress = new Uri("https://localhost:7119/api");
    })
    .AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddRefitClient<IFileApi>()
    .ConfigureHttpClient(configuration =>
    {
        configuration.BaseAddress = baseApiAddress;
    })
    .AddHttpMessageHandler<AuthHeaderHandler>();

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
        configuration.BaseAddress = new Uri("https://localhost:7017/api");
    })
    .AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddTransient<AuthHeaderHandler>();
builder.Services.AddTransient<SignInHandler>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();


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