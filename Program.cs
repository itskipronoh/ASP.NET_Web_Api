using NSwag.AspNetCore;
using ContosoPizza.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add PostgreSQL DbContext
builder.Services.AddDbContext<PizzaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register NSwag so we can host a Swagger UI pointing at the OpenAPI JSON
builder.Services.AddSwaggerDocument();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Expose the Microsoft-generated OpenAPI JSON at /openapi/v1.json
    app.MapOpenApi();

    // Serve NSwag-based Swagger UI and point it to the MapOpenApi JSON
    app.UseOpenApi();
    app.UseSwaggerUi(settings =>
    {
        settings.Path = "/swagger";
        // Tell the UI where the OpenAPI JSON is located
        settings.DocumentPath = "/openapi/v1.json";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
