using MusicCatalog.Application.Shared;
using MusicCatalog.Domain;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.Application.Songs;

public sealed class SongService : CrudService<Song>, ISongService
{
    private readonly ISongRepository _songRepository;

    public SongService(ISongRepository songRepository) : base(songRepository)
    {
        _songRepository = songRepository;
    }

    public async Task<IReadOnlyCollection<Song>> GetByArtistAsync(int artistId, CancellationToken cancellationToken)
    {
        return await _songRepository.GetByArtistAsync(artistId, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Song>> GetByGenreAsync(int genreId, FilterPaginationDto dto, CancellationToken cancellationToken)
    {
        return await _songRepository.GetByGenreAsync(genreId, dto, cancellationToken);
    }

}