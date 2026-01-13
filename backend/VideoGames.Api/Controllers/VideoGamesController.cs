using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGames.Api.Data;
using VideoGames.Api.Models;

namespace VideoGames.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoGamesController : ControllerBase
{
    private readonly AppDbContext _db;

    public VideoGamesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<VideoGame>>> GetAll()
    {
        var games = await _db.VideoGames
            .OrderBy(g => g.Title)
            .ToListAsync();

        return Ok(games);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<VideoGame>> GetById(int id)
    {
        var game = await _db.VideoGames.FindAsync(id);
        return game is null ? NotFound() : Ok(game);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, VideoGame updated)
    {
        if (id != updated.Id) return BadRequest("Route id must match body id.");

        var existing = await _db.VideoGames.FindAsync(id);
        if (existing is null) return NotFound();

        if (string.IsNullOrWhiteSpace(updated.Title)) return BadRequest("Title is required.");
        if (string.IsNullOrWhiteSpace(updated.Platform)) return BadRequest("Platform is required.");

        existing.Title = updated.Title;
        existing.Platform = updated.Platform;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _db.VideoGames.FindAsync(id);
        if (existing is null) return NotFound();

        _db.VideoGames.Remove(existing);
        await _db.SaveChangesAsync();
        return NoContent();
    }

}
