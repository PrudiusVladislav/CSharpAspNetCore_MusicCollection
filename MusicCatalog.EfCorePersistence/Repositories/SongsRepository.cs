using Microsoft.EntityFrameworkCore;
using MusicCatalog.Application.Songs;
using MusicCatalog.Domain;
using MusicCatalog.Domain.Models;
using MusicCatalog.EfCorePersistence.Data;

namespace MusicCatalog.EfCorePersistence.Repositories;

public class SongsRepository: CrudRepository<Song>, ISongRepository
{
    public SongsRepository(MusicCatalogDbContext dbContext) : base(dbContext)
    {
    }

    protected override void Update(Song model, Song entity)
    {
        entity.Title = model.Title;
        entity.Duration = model.Duration;
        entity.ReleaseDate = model.ReleaseDate;
        entity.GenreId = model.GenreId;
    }

    public async Task<IReadOnlyCollection<Song>> GetByGenreAsync(int genreId, FilterPaginationDto dto, CancellationToken cancellationToken)
    {
        return await DbContext.Songs
            .Where(song => song.GenreId.HasValue && song.GenreId.Value == genreId)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<Song>> GetByArtistAsync(int artistId, CancellationToken cancellationToken)
    {
        return await DbContext.Songs
            .Where(song => song.Artists!.Any(artist => artist.Id == artistId))
            .ToArrayAsync(cancellationToken);
    }
}