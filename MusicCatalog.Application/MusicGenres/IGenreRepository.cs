using MusicCatalog.Application.Shared;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.Application.MusicGenres;

public interface IGenreRepository: ICrudRepository<MusicGenre>
{
    
}