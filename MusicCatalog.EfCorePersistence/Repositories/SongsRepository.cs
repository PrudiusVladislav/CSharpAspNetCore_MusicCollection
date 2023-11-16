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
    
    protected override IQueryable<Song> Filter(IQueryable<Song> query, string filter)
    {
        return query.Where(s => s.Title.Contains(filter));
    }

    protected override IQueryable<Song> Sort(IQueryable<Song> query, string orderBy, bool isAscending)
    {
        return orderBy.ToLower() switch
        {
            "title" => isAscending ? query.OrderBy(m => m.Title) : query.OrderByDescending(m => m.Title),
            "releasedate" => isAscending ? query.OrderBy(m => m.ReleaseDate) : query.OrderByDescending(m => m.ReleaseDate),
            _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id)
        };
    }
    
    protected override void Update(Song model, Song entity)
    {
        entity.Title = model.Title;
        entity.Duration = model.Duration;
        entity.ReleaseDate = model.ReleaseDate;
        entity.GenreId = model.GenreId;
    }

    protected override IQueryable<Song> Include(IQueryable<Song> query)
    {
        return query.Include(s => s.Artists);
    }
}