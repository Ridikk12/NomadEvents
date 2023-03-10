using FluentValidation;
using Microsoft.OpenApi.Models;
using NomadEvents.MinimalAPI.Application.Endpoints;
using NomadEvents.MinimalAPI.Application.Endpoints.GetEvent;
using NomadEvents.MinimalAPI.Application.Endpoints.LoginDetails;
using NomadEvents.MinimalAPI.Infrastructure;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    configuration.WriteTo.Console();
});

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

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

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

app.MapPost("api/event/", AddNomadEventEndpoint.AddNomadEvent).RequireAuthorization()
    .WithDisplayName("Add Event");
app.MapPost("api/login", LoginEndpoint.Login).WithName("Login");
app.MapGet("api/login/details", LoginDetailsEndpoint.GetLoginDetails).RequireAuthorization();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionLoggingMiddleware>();

app.Run();