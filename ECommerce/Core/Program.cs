
using DataBaseLayer;
using DTO;
using DTO.Constant;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{ // config authontication by JWT more info can visit https://jwt.io/introduction
    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true, // valied to lissen to this end point
        ValidateAudience = true,
        ValidIssuer = AuthSetting.Issuer,
        ValidAudience = AuthSetting.Audience,
        RequireExpirationTime = true, // must define specific time to expire the token after Login
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthSetting.Key)), // Key  that use to hash 
        ValidateIssuerSigningKey = true,
    };
});

builder.Services.AddAuthorization(options =>
{

    options.AddPolicy(Roles.Admin,
        authBuilder =>
        {
            authBuilder.RequireRole(Roles.Admin);
        });


});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowSpecificOrigins",
        builder =>
        {
            //builder.WithOrigins("http://localhost:3001", "http://localhost:3000", "http://localhost:4200")
            //.AllowAnyHeader().AllowAnyMethod();
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterYourLibrary();
builder.Services.RegisterYourServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors("_myAllowSpecificOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
