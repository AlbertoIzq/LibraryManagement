using LibraryManagement.API.Data;
using LibraryManagement.API.Middlewares;
using LibraryManagement.API.Repositories;
using LibraryManagement.API.Services;
using LibraryManagement.Business.Interfaces;
using LibraryManagement.Business.Mappings;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog.Events;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configure the SQLite in-memory database
// Create and open a shared SQLite connection for in-memory database
var sqliteConnection = new SqliteConnection("DataSource=:memory:");
sqliteConnection.Open(); // Keep the connection open throughout the app's lifetime

builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlite(sqliteConnection) //
           .EnableSensitiveDataLogging()
           .EnableDetailedErrors());

// Dependency injection for additional services.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// Add Serilog.
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Information()
    // To avoid logging irrelevant information from Microsoft
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    // To log as information from Microsoft only lifetime events
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddHostedService<OverdueNotificationService>();

var app = builder.Build();

// Ensure the database is created and initialized
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
    context.Database.Migrate(); // Apply migrations
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Concert API");
        options.WithTheme(ScalarTheme.Saturn);
        options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();