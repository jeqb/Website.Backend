using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Website.Backend.Domain.Repositories.Factories;
using Website.Backend.Infrastructure.Cryptography;
using Website.Backend.Infrastructure.Email;
using Website.Backend.Infrastructure.Finance;
using Website.Backend.Services;
using Website.Backend.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Cors
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithExposedHeaders("WWW-Authenticate")
            .WithOrigins("http://localhost:3000")
            .WithOrigins("https://jeqb-frontend.azurewebsites.net")
            .WithOrigins("https://jamesbonnerproject.com")
            .WithOrigins("https://www.jamesbonnerproject.com")
            .AllowCredentials();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// https://www.c-sharpcorner.com/article/jwt-json-web-token-authentication-in-asp-net-core/
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


builder.Services.AddHttpClient();

// this project
builder.Services.AddSingleton<ILoginService, LoginService>();
builder.Services.AddSingleton<IMessageService, MessageService>((serviceProvider) =>
{
    // not sure how I feel about reading from disk every instantiation.
    StaticFileLoader staticFileLoader = new StaticFileLoader();
    string thankYouEmailBody = staticFileLoader.GetFileString("ThankYouEmailBody.html");
    string ownerEmailBody = staticFileLoader.GetFileString("OwnerEmailNotificationTemplate.html");

    return new MessageService(
        serviceProvider.GetService<ILogger<MessageService>>(),
        serviceProvider.GetService<IRepositoryFactory>(),
        serviceProvider.GetService<IEmailNotificationService>(),
        serviceProvider.GetService<IConfiguration>()["Notifications:OwnerEmail"],
        thankYouEmailBody,
        ownerEmailBody
        );
});
builder.Services.AddSingleton<IFinancialService, FinancialService>();

// Domain
builder.Services.AddSingleton<IRepositoryFactory, StorageTableRepositoryFactory>((serviceProvider) =>
{
    return new StorageTableRepositoryFactory(
            serviceProvider.GetService<IConfiguration>()["AzureTableStorage:StorageUri"],
            serviceProvider.GetService<IConfiguration>()["AzureTableStorage:StorageAccountName"],
            serviceProvider.GetService<IConfiguration>()["AzureTableStorage:StorageAccountKey"],
            serviceProvider.GetService<IConfiguration>()["AzureTableStorage:StorageTableName"]
            );
});

// infrastructure
builder.Services.AddSingleton<ICryptoCurrencyService, YahooBtcPriceScraper>();
builder.Services.AddSingleton<IGoldService, KitcoGoldSpotPriceScraper>();
builder.Services.AddSingleton<IStockMarketService, MarketWatchScraper>();
builder.Services.AddSingleton<ICryptographyUtility, CryptographyUtility>((serviceProvider) =>
{
    return new CryptographyUtility(
            serviceProvider.GetService<IConfiguration>()["Jwt:Key"],
            serviceProvider.GetService<IConfiguration>()["Jwt:Issuer"]
            );
});
builder.Services.AddSingleton<IEmailNotificationService, EmailNotificationService>((serviceProvider) =>
{
    return new EmailNotificationService(
        serviceProvider.GetService<IConfiguration>()["AzureTableStorage:QueueStorageConnectionString"],
        serviceProvider.GetService<IConfiguration>()["AzureTableStorage:QueueName"]
        );
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

// order matters
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
