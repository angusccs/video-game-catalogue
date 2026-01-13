using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGames.Api.Controllers;
using VideoGames.Api.Data;
using VideoGames.Api.Models;
using Xunit;

public class VideoGamesControllerTests
{
    private static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task GetAll_WhenDatabaseHasGames_ReturnsList()
    {
        using var context = CreateContext();
        context.VideoGames.Add(new VideoGame
        {
            Id = 1,
            Platform = "Platform",
            Title = "Title",
        });
        await context.SaveChangesAsync();

        var controller = new VideoGamesController(context);

        var result = await controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var games = Assert.IsType<List<VideoGame>>(ok.Value);

        Assert.Single(games);
        Assert.Equal("Title", games[0].Title);
    }

    [Fact]
    public async Task GetById_WhenMissing_ReturnsNotFound()
    {
        using var context = CreateContext();
        var controller = new VideoGamesController(context);

        var result = await controller.GetById(999);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Update_WhenGameExists_PersistsChanges()
    {
        using var context = CreateContext();
        context.VideoGames.Add(new VideoGame
        {
            Id = 2,
            Platform = "Platform",
            Title = "Title",
        });
        await context.SaveChangesAsync();

        var controller = new VideoGamesController(context);

        var update = new VideoGame
        {
            Id = 2,
            Platform = "Platform2",
            Title = "Title2",
        };

        var updateResult = await controller.Update(2, update);

        var updated = await context.VideoGames.FindAsync(2);
        Assert.Equal("Title2", updated.Title);
        Assert.Equal("Platform2", updated.Platform);
    }
}
