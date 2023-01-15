using System.Security.Claims;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NomadEvents.MinimalAPI.Application.Endpoints;
using NomadEvents.MinimalAPI.Application.Endpoints.GetEvent;
using NomadEvents.MinimalAPI.Application.Endpoints.LoginDetails;
using NomadEvents.MinimalAPI.Domain.Entities;
using NomadEvents.MinimalAPI.Infrastructure;
using SQLitePCL;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
        Name = "Bearer",
        BearerFormat = "JWT",
        Scheme = "bearer",
        Description = "Specify the authorization token.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddDatabase(builder);
builder.Services.AddValidatorsFromAssembly(typeof(AddNomadEventValidator).Assembly);
builder.Services.AddInfraServices();
builder.Services.AddAuth(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("api/event/", AddNomadEventEndpoint.AddNomadEvent).RequireAuthorization();
app.MapPost("api/login", LoginEndpoint.Login);
app.MapGet("api/login/details", LoginDetailsEndpoint.GetLoginDetails).RequireAuthorization();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

public class NomadEventDbContextFactory : IDesignTimeDbContextFactory<NomadEventDbContext>
{
    public NomadEventDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<NomadEventDbContext>();
        optionsBuilder.UseSqlServer("./");

        return new NomadEventDbContext(optionsBuilder.Options);
    }
}