using MusicCatalog.Application.Shared;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.Application.MusicGenres;

public sealed class GenreService: CrudService<MusicGenre>, IGenreService
{
    public GenreService(IGenreRepository genreRepository) : base(genreRepository)
    {
    }

}