namespace MusicCatalog.Domain.Models;

public class Song : Model
{
    public required string Title { get; set; }
    //duration in seconds
    public int? Duration { get; set; }
    public int? GenreId { get; set; }
    public MusicGenre? Genre { get; set; }
    public DateTime ReleaseDate { get; set; }
    public ICollection<Artist>? Artists { get; set; } = new List<Artist>();
    
    public override bool IsMatch(string searchTerm)
    {
        return Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
               Genre?.IsMatch(searchTerm) is true ||
               Artists?.Any(artist => artist.IsMatch(searchTerm)) is true;
    }

    public override object? SortBy(string sortColumn)
    {
        return sortColumn switch
        {
            nameof(Title) => Title,
            nameof(Duration) => Duration,
            nameof(ReleaseDate) => ReleaseDate,
            _ => Id
        };
    }

    public override string ToString()
    {
        return $"{Title} ({(Duration / 60):D2}:{Duration % 60})";
    }

    public string GetFormattedDuration()
    {
        return $"{Duration / 60:D2}:{Duration % 60:D2}";
    }
}