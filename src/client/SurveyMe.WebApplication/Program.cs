using Refit;
using SurveyMe.Data.Abstracts;
using SurveyMe.Services;
using SurveyMe.Services.Abstracts;
using SurveyMe.WebApplication.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

builder.Services.AddRefitClient<IUserApi>()
    .ConfigureHttpClient(configuration =>
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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        "default",
        "{controller=Users}/{action=Index}/{id?}");
});

app.Run();