using Refit;
using SurveyMe.Data;
using SurveyMe.Data.Abstracts;
using SurveyMe.Services;
using SurveyMe.Services.Abstracts;
using SurveyMe.WebApplication.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddHttpClient<IClient, Client>(configuration =>
{
    configuration.BaseAddress = new Uri(builder.Configuration["ApiConfiguration:BaseAddress"]);
});

builder.Services.AddRefitClient<IFileApi>()
    .ConfigureHttpClient(configuration =>
    {
        configuration.BaseAddress = new Uri(builder.Configuration["ApiConfiguration:BaseAddress"]);
    });

builder.Services.AddRefitClient<ISurveyApi>()
    .ConfigureHttpClient(configuration =>
    {
        configuration.BaseAddress = new Uri(builder.Configuration["ApiConfiguration:BaseAddress"]);
    });



builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IUserApi, UserApi>();
builder.Services.AddScoped<IAccountService, AccountApi>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseCustomExceptionHandler();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Users}/{action=Index}/{id?}");

app.Run();