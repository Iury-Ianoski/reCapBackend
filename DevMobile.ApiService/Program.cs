using DevMobile.ApiService.Dbcontext;
using DevMobile.ApiService.Endpoints;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddNpgsqlDataSource("postgres");

// Add services to the container.
builder.Services.AddProblemDetails();

// Register the DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgres")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapDefaultEndpoints();

app.MapBooksEndpoints();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevMobile API V1");
    c.DocExpansion(DocExpansion.None);
});

// Add a test endpoint for database connection
app.MapGet("/test-db", async (AppDbContext db) =>
{
    try
    {
        var canConnect = await db.Database.CanConnectAsync();
        return canConnect ? "Database connection successful" : "Database connection failed";
    }
    catch (Exception ex)
    {
        return $"Database connection failed: {ex.Message}";
    }
});

app.Run();
