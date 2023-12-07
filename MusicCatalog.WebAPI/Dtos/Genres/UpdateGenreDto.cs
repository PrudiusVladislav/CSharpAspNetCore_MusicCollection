using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.WebAPI.Dtos.Genres;

public sealed class UpdateGenreDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; } = null!;
}