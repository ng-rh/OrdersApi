using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Read PostgreSQL connection info from environment variables
var host = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "postgresql";
var port = Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5432";
var dbName = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "ordersdb";
var username = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "demo";
var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "demo";

var connectionString = $"Host={host};Port={port};Database={dbName};Username={username};Password={password}";

builder.Services.AddDbContext<OrdersDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger only in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Apply EF Core migrations at startup
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database migration failed: {ex.Message}");
    }
}

app.Run();
