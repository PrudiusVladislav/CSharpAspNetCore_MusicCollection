using MusicCatalog.Application.MusicGenres;
using MusicCatalog.Domain.Models;
using MusicCatalog.EfCorePersistence.Data;

namespace MusicCatalog.EfCorePersistence.Repositories;

public sealed class GenresRepository: CrudRepository<MusicGenre>, IGenreRepository
{
    public GenresRepository(MusicCatalogDbContext dbContext) : base(dbContext)
    {
    }

    protected override void Update(MusicGenre model, MusicGenre entity)
    {
        entity.Name = model.Name;
    }
}