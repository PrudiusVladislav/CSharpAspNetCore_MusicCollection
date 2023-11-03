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

    public async Task<IReadOnlyCollection<Artist>> GetBySongAsync(int songId, CancellationToken cancellationToken)
    {
        return await DbContext.Artists
            .Include(a => a.Songs)
            .Where(a => a.Songs!.Any(s => s.Id == songId))
            .ToArrayAsync(cancellationToken);
    }

}