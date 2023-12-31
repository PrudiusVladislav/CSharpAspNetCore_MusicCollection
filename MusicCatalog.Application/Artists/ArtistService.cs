using MusicCatalog.Application.Shared;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.Application.Artists;

public sealed class ArtistService : CrudService<Artist>, IArtistService
{
    private readonly IArtistRepository _artistRepository;

    public ArtistService(IArtistRepository artistRepository) : base(artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public async Task<IReadOnlyCollection<Artist>> GetBySongAsync(int songId, CancellationToken cancellationToken)
    {
        return await _artistRepository.GetBySongAsync(songId, cancellationToken);
    }
}