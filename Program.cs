using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Read PostgreSQL connection info from environment variables
var host = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "postgresql";
var port = Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5432";
var dbName = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "ordersdb";
var username = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "demo";
var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "demo";

var connectionString = $"Host={host};Port={port};Database={dbName};Username={username};Password={password}";

// Register DbContext with PostgreSQL
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

// Automatically apply EF Core migrations at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();
    try
    {
        db.Database.Migrate(); // Creates the database and tables if they don't exist
        Console.WriteLine("Database migration applied successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database migration failed: {ex.Message}");
        throw; // optional: rethrow to fail app startup if migration fails
    }
}

app.Run();
