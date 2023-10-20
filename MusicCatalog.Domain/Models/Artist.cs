namespace MusicCatalog.Domain.Models;

public class Artist : Model
{
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<Song>? Songs { get; set; } = new List<Song>();


    public override bool IsMatch(string searchTerm)
    {
        return FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
               LastName?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) is true ||
               Songs?.Any(song => song.IsMatch(searchTerm)) is true;
    }

    public override object? SortBy(string sortColumn)
    {
        return sortColumn switch
        {
            nameof(FirstName) => FirstName,
            nameof(LastName) => LastName,
            nameof(DateOfBirth) => DateOfBirth,
            _ => Id
        };
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }

}