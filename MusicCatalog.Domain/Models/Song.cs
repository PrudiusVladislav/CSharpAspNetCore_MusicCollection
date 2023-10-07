namespace MusicCatalog.Domain.Models;

public class Song : Model
{
    public required string Title { get; set; }
    public string? Description { get; set; }

    public ICollection<Artist> Authors { get; set; } = new List<Artist>();
}