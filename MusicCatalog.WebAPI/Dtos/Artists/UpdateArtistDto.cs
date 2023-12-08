using MusicCatalog.Domain.Models;

namespace MusicCatalog.WebAPI.Dtos.Artists;

public class UpdateArtistDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public ICollection<Song> Songs { get; set; } = null!;
}