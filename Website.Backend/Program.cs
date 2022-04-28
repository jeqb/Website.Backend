using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Website.Backend.Domain.Repositories;
using Website.Backend.Domain.Repositories.Interfaces;
using Website.Backend.Infrastructure;
using Website.Backend.Infrastructure.Interfaces;
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
            .WithOrigins("https://jeqb-frontend.azurewebsites.net/")
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
builder.Services.AddSingleton<IMessageService, MessageService>();
builder.Services.AddSingleton<IFinancialService, FinancialService>();

// Domain
builder.Services.AddSingleton<IRepositoryFactory, RepositoryFactory>((serviceProvider) =>
{
    return new RepositoryFactory(
            serviceProvider.GetService<IConfiguration>()["AzureTableStorage:StorageUri"],
            serviceProvider.GetService<IConfiguration>()["AzureTableStorage:StorageAccountName"],
            serviceProvider.GetService<IConfiguration>()["AzureTableStorage:StorageAccountKey"]
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
