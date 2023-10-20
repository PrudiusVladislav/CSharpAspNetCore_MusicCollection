using MusicCatalog.Application.Shared;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.Application.Artists;

public interface IArtistService : ICrudService<Artist>
{
    Task<IReadOnlyCollection<Artist>> GetBySongAsync(int songId, CancellationToken cancellationToken);
}