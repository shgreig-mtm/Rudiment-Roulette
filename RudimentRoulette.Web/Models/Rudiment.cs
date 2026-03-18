namespace RudimentRoulette.Web.Models;

public class Rudiment
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Sticking { get; set; } = string.Empty;
    public DifficultyLevel Difficulty { get; set; }
    // Note subdivision: "8th", "16th", "triplet", etc.
    // Defaults to "16th" for most rudiments
    public string Subdivision { get; set; } = "16th";
    // Path to sheet music image (e.g., "/images/rudiments/single-stroke-roll.png")
    public string? SheetMusicUrl { get; set; }
}
