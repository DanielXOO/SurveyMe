using Refit;
using SurveyMe.Data;
using SurveyMe.Data.Abstracts;
using SurveyMe.Services;
using SurveyMe.Services.Abstracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

builder.Services.AddHttpClient<IUserApi, UserApi>(configuration =>
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
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}
//app.UseCustomExceptionHandler();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Users}/{action=Index}/{id?}");

app.Run();