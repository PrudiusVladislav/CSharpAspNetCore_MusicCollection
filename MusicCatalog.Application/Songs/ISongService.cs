using MusicCatalog.Application.Shared;
using MusicCatalog.Domain;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.Application.Songs;

public interface ISongService : ICrudService<Song>
{
    Task<IReadOnlyCollection<Song>> GetByGenreAsync(int genreId, FilterPaginationDto dto, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Song>> GetByArtistAsync(int artistId, CancellationToken cancellationToken);

}