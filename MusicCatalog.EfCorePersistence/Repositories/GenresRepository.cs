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
    
    protected override IQueryable<MusicGenre> Filter(IQueryable<MusicGenre> query, string filter)
    {
        return query.Where(g => g.Name.Contains(filter));
    }

    protected override IQueryable<MusicGenre> Sort(IQueryable<MusicGenre> query, string orderBy, bool isAscending)
    {
        return orderBy.ToLower() switch
        {
            "name" => isAscending ? query.OrderBy(g => g.Name) : query.OrderByDescending(g => g.Name),
            _ => isAscending ? query.OrderBy(g => g.Id) : query.OrderByDescending(g => g.Id)
        };
    }

}