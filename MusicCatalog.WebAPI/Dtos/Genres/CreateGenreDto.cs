using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.WebAPI.Dtos.Genres;

public sealed class CreateGenreDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; } = null!;
}