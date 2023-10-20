using MusicCatalog.Application.Artists;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.MemoryPersistence.Repositories;

public sealed class ArtistRepository : CrudRepository<Artist>, IArtistRepository
{
    public Task<IReadOnlyCollection<Artist>> GetBySongAsync(int songId, CancellationToken cancellationToken)
    {
        var songs = Models.Values
            .Where(artist => artist.Songs?.Any(song => song.Id == songId) is true)
            .ToArray();
        return Task.FromResult<IReadOnlyCollection<Artist>>(songs);
    }
    
    protected override void UpdateModel(Artist oldModel, Artist newModel)
    {
        oldModel.FirstName = newModel.FirstName;
        oldModel.LastName = newModel.LastName;
        oldModel.DateOfBirth = newModel.DateOfBirth;
    }
}