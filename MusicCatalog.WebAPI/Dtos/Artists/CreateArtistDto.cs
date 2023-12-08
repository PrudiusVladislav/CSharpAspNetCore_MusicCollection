using MusicCatalog.Domain.Models;

namespace MusicCatalog.WebAPI.Dtos.Artists;

public class CreateArtistDto
{
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<Song>? Songs { get; set; } = new List<Song>();
}