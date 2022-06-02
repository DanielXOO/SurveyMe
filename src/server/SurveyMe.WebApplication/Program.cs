using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SurveyMe.Common.Microsoft.Logging;
using SurveyMe.Data;
using SurveyMe.Data.Stores;
using SurveyMe.Common.Time;
using SurveyMe.DomainModels.Roles;
using SurveyMe.DomainModels.Users;
using SurveyMe.Foundation.MapperConfigurations.Profiles;
using SurveyMe.Foundation.Models.Configurations;
using SurveyMe.Foundation.Services;
using SurveyMe.Foundation.Services.Abstracts;
using SurveyMe.Foundation.Validators;
using SurveyMe.WebApplication.Converters;
using SurveyMe.WebApplication.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logBuilder =>
{
    logBuilder.AddLogger();
    logBuilder.AddFile(builder.Configuration.GetSection("Serilog:FileLogging"));
});

var version = builder.Configuration["SwaggerConfiguration:ApiVersion"];

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    o.JsonSerializerOptions.Converters.Add(new AnswerJsonConverter());
    o.JsonSerializerOptions.Encoder = JavaScriptEncoder
        .Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic);
});
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc($"api-v{version}", new OpenApiInfo
    {
        Title = "SurveyMe Api",
        Version = version,
        Contact = new OpenApiContact
        {
            Name = "DanielXOO",
            Email = "dankulinkovich@gmail.com",
            Url = new Uri("https://github.com/DanielXOO")
        }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    
    var filePath = Path.Combine(AppContext.BaseDirectory, "SurveyMe.WebApplication.xml");
    options.IncludeXmlComments(filePath);
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<FileServiceConfiguration>(builder.Configuration.GetSection("FileService"));
builder.Services.Configure<TokenGeneratorConfiguration>(builder.Configuration.GetSection("JwtTokenGeneratorService"));

builder.Services.AddDbContext<SurveyMeDbContext>(options
    => options.UseSqlServer(builder.Configuration
        .GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISurveyMeUnitOfWork, SurveyMeUnitOfWork>();

builder.Services.AddAutoMapper(configuration =>
{
    configuration.AddMaps(typeof(Program).Assembly);
    configuration.AddProfile<SurveyStatisticProfile>();
});


builder.Services.AddIdentityCore<User>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddPasswordValidator<PasswordValidator>()
    .AddUserStore<UserStore>()
    .AddRoles<Role>()
    .AddRoleStore<RoleStore>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var configuration = builder.Configuration
            .GetSection("JwtTokenGeneratorService").Get<TokenGeneratorConfiguration>();
        
        //TODO: Change Audience and Issuer and enable validation
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidIssuer = configuration.Issuer,
            ValidateAudience = false,
            ValidAudience = configuration.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = configuration.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
        
        options.SaveToken = true;
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<ISurveyMeUnitOfWork, SurveyMeUnitOfWork>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddScoped<ISurveyAnswersService, SurveySurveyAnswersService>();
builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddSingleton<ITokenGenerator, TokenGenerator>();
builder.Services.AddSingleton<ISystemClock, SystemClock>();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/api-v{version}/swagger.json", $"api-v{version}");
    });
    app.UseCustomExceptionHandler();
}

await app.Services.CreateDbIfNotExists();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();