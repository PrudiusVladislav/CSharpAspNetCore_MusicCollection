using MusicCatalog.Application.Shared;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.Application.Artists;

public interface IArtistRepository : ICrudRepository<Artist>
{
    Task<IReadOnlyCollection<Artist>> GetBySongAsync(int songId, CancellationToken cancellationToken);
}