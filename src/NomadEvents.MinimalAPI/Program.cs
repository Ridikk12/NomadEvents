using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using NomadEvents.MinimalAPI.Application.Endpoints;
using NomadEvents.MinimalAPI.Domain.Entities;
using NomadEvents.MinimalAPI.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabase(builder);
builder.Services.AddValidatorsFromAssembly(typeof(AddNomadEventValidator).Assembly);
builder.Services.AddInfraServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("api/event/", AddNomadEventEndpoint.AddNomadEvent);


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