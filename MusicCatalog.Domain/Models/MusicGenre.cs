namespace MusicCatalog.Domain.Models;

public class MusicGenre: Model
{
    public required string CategoryName { get; set; }
    public ICollection<Song>? Songs { get; set; } = new List<Song>();
    public override bool IsMatch(string searchTerm)
    {
        return CategoryName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
               Songs?.Any(song => song.IsMatch(searchTerm)) is true;
    }

    public override object? SortBy(string sortColumn)
    {
        return sortColumn switch
        {
            nameof(CategoryName) => CategoryName,
            _ => Id
        };
    }

    public override string ToString()
    {
        return CategoryName;
    }

}