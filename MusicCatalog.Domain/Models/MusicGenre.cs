namespace MusicCatalog.Domain.Models;

public class MusicGenre: Model
{
    public required string Name { get; set; }
    public ICollection<Song>? Songs { get; set; } = new List<Song>();
    public override bool IsMatch(string searchTerm)
    {
        return Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
               Songs?.Any(song => song.IsMatch(searchTerm)) is true;
    }

    public override object? SortBy(string sortColumn)
    {
        return sortColumn switch
        {
            nameof(Name) => Name,
            _ => Id
        };
    }

    public override string ToString()
    {
        return Name;
    }

}