using MusicCatalog.Domain;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.Application.Songs;

public interface ISongService
{
    Task<int> CreateAsync(Song song,
        CancellationToken cancellationToken);
    Task UpdateAsync(Song song,
        CancellationToken cancellationToken);
    Task DeleteAsync(int id,
        CancellationToken cancellationToken);
    Task<Song> GetAsync(int id,
        CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Song>> GetAllAsync(FilterPaginationDto dto,
        CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Song>> GetByCategoryAsync(int categoryId, 
        FilterPaginationDto dto,
        CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Song>> GetByArtistAsync(int artistId,
        CancellationToken cancellationToken);
}