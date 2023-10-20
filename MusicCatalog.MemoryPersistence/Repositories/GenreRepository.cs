using MusicCatalog.Application.MusicGenres;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.MemoryPersistence.Repositories;

public sealed class GenreRepository : CrudRepository<MusicGenre>, IGenreRepository
{
    protected override void UpdateModel(MusicGenre oldModel, MusicGenre newModel)
    {
        oldModel.Name = newModel.Name;
    }
}