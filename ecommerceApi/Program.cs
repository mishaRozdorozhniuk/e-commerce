using System.Text;
using ecommerceApi.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDatabase(builder.Configuration);

builder.Services
           .AddAuthentication(options =>
           {
               options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           })
           .AddJwtBearer(jwt =>
           {
               var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fa5DRdkVwZeQnrDAcBrHCYwAWd6y2crPUbSZq4zUWBRFwDfKDXQWH38vZRfv"));

               jwt.SaveToken = true;

               jwt.TokenValidationParameters = new TokenValidationParameters()
               {
                   IssuerSigningKey = issuerSigningKey,
                   ValidIssuer = "http://localhost:3001",
                   ValidAudience = "http://localhost:5000",
                   ValidateIssuer = true,
                   ValidateAudience = true
               };
           });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
