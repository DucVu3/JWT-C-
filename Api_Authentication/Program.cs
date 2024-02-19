using Api_Authentication.DataContext;
using Api_Authentication.Entities;
using Api_Authentication.Payloads.Converters;
using Api_Authentication.Payloads.DTOs;
using Api_Authentication.Payloads.Responses;
using Api_Authentication.Services.Implements;
using Api_Authentication.Services.Interfaces;
using Fluent.Infrastructure.FluentModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Auth", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Làm theo mẫu này. Ví dụ: Bearer{Token}",
        Name = "Authorizaton",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey

    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:SecretKey").Value))

    };
});
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<ResponseObject<UserDTO>>();
builder.Services.AddSingleton<UserConverter>();
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
