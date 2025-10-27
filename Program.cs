using NSwag.AspNetCore;
using ContosoPizza.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Npgsql to support JSON serialization for List<string>
var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
dataSourceBuilder.EnableDynamicJson();
var dataSource = dataSourceBuilder.Build();

// Add PostgreSQL DbContext with JSON support
builder.Services.AddDbContext<PizzaDbContext>(options =>
    options.UseNpgsql(dataSource));

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
