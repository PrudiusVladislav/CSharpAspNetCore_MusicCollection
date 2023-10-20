using MusicCatalog.Application.Shared;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.Application.Artists;

public interface IArtisRepository : ICrudRepository<Artist>
{
    Task<IReadOnlyCollection<Artist>> GetBySongAsync(int songId, CancellationToken cancellationToken);
}