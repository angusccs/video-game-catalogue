using Microsoft.EntityFrameworkCore;
using VideoGames.Api.Data;
using VideoGames.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Controllers (Web API endpoints)
builder.Services.AddControllers();

// EF Core + SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS so Angular (localhost:4200) can call the API during development
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// Use CORS policy (safe to keep for this assignment)
app.UseCors("DevCors");

// Map controller routes
app.MapControllers();

// Create database + seed sample data on startup (Code First)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.VideoGames.Any())
    {
        db.VideoGames.AddRange(
            new VideoGame { Title = "The Legend of Zelda", Platform = "Switch" },
            new VideoGame { Title = "Halo", Platform = "Xbox" },
            new VideoGame { Title = "God of War", Platform = "PlayStation" }
        );

        db.SaveChanges();
    }
}

app.Run();
