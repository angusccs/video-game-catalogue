namespace VideoGames.Api.Models;

public class VideoGame
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Platform { get; set; }
}
