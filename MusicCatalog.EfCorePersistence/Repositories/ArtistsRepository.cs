using Microsoft.EntityFrameworkCore;
using MusicCatalog.Application.Artists;
using MusicCatalog.Domain.Models;
using MusicCatalog.EfCorePersistence.Data;

namespace MusicCatalog.EfCorePersistence.Repositories;

public sealed class ArtistsRepository: CrudRepository<Artist>, IArtistRepository
{
    public ArtistsRepository(MusicCatalogDbContext dbContext) : base(dbContext)
    {
    }

    protected override void Update(Artist model, Artist entity)
    {
        entity.FirstName = model.FirstName;
        entity.LastName = model.LastName;
        entity.DateOfBirth = model.DateOfBirth;
    }

    protected override IQueryable<Artist> Filter(IQueryable<Artist> query, string filter)
    {
        return query.Where(a => a.FirstName.Contains(filter) || (a.LastName != null && a.LastName.Contains(filter)));
    }

    protected override IQueryable<Artist> Sort(IQueryable<Artist> query, string orderBy, bool isAscending)
    {
        return orderBy.ToLower() switch
        {
            "firstname" => isAscending ? query.OrderBy(a => a.FirstName) : query.OrderByDescending(a => a.FirstName),
            "lastname" => isAscending ? query.OrderBy(a => a.LastName) : query.OrderByDescending(a => a.LastName),
            "dateofbirth" => isAscending ? query.OrderBy(a => a.DateOfBirth) : query.OrderByDescending(a => a.DateOfBirth),
            _ => isAscending ? query.OrderBy(a => a.Id) : query.OrderByDescending(a => a.Id)
        };

    }

    public async Task<IReadOnlyCollection<Artist>> GetBySongAsync(int songId, CancellationToken cancellationToken)
    {
        return await DbContext.Artists
            .Include(a => a.Songs)
            .Where(a => a.Songs!.Any(s => s.Id == songId))
            .ToArrayAsync(cancellationToken);
    }

}