using Microsoft.EntityFrameworkCore;
using OrdersApi.Data; // Update with your actual DbContext namespace

var builder = WebApplication.CreateBuilder(args);

// Read PostgreSQL connection info from environment variables
var host = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "postgresql";
var port = Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5432";
var dbName = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "ordersdb";
var username = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "demo";
var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "demo";

var connectionString = $"Host={host};Port={port};Database={dbName};Username={username};Password={password}";

// Add DbContext with PostgreSQL
builder.Services.AddDbContext<OrdersDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger UI only in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Apply EF Core
