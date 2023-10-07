namespace MusicCatalog.Domain.Models;

public class MusicCategory
{
    public required string CategoryName { get; set; }
    public ICollection<Song> Songs { get; set; } = new List<Song>();
}