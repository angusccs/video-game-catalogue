using Microsoft.EntityFrameworkCore;
using VideoGames.Api.Models;

namespace VideoGames.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<VideoGame> VideoGames => Set<VideoGame>();

}
