using MusicCatalog.Application.Songs;
using MusicCatalog.Domain;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.MemoryPersistence.Repositories;

public class SongRepository: CrudRepository<Song>, ISongRepository
{
    protected override void UpdateModel(Song oldModel, Song newModel)
    {
        oldModel.Title = newModel.Title;
        oldModel.Duration = newModel.Duration;
        oldModel.ReleaseDate = newModel.ReleaseDate;
        oldModel.GenreId = newModel.GenreId;
        oldModel.Genre = newModel.Genre;
        oldModel.Artists = newModel.Artists;
    }

    public Task<IReadOnlyCollection<Song>> GetByGenreAsync(int genreId, FilterPaginationDto dto, CancellationToken cancellationToken)
    {
        var songs = Models.Values
            .Where(song => song.GenreId.HasValue && song.GenreId.Value == genreId)
            .ToArray();
        return Task.FromResult<IReadOnlyCollection<Song>>(songs);

    }

    public Task<IReadOnlyCollection<Song>> GetByArtistAsync(int artistId, CancellationToken cancellationToken)
    {
        var songs = Models.Values
            .Where(song => song.Artists?.Any(artist => artist.Id == artistId) is true)
            .ToArray();
        return Task.FromResult<IReadOnlyCollection<Song>>(songs);

    }
}