using MusicCatalog.Domain;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.Application.Songs;

public class SongService: ISongService
{
    private readonly ISongRepository _songRepository;

    public SongService(ISongRepository songRepository)
    {
        _songRepository = songRepository;
    }

    public async Task<int> CreateAsync(Song song, CancellationToken cancellationToken)
    {
        return await _songRepository.AddAsync(song, cancellationToken);
    }

    public async Task UpdateAsync(Song song, CancellationToken cancellationToken)
    {
        await _songRepository.UpdateAsync(song, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _songRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<Song> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await _songRepository.GetAsync(id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Song>> GetAllAsync(FilterPaginationDto dto, CancellationToken cancellationToken)
    {
        return await _songRepository.GetAllAsync(dto, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Song>> GetByCategoryAsync(int categoryId, FilterPaginationDto dto, CancellationToken cancellationToken)
    {
        return await _songRepository.GetByCategoryAsync(categoryId, dto, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Song>> GetByArtistAsync(int artistId, CancellationToken cancellationToken)
    {
        return await _songRepository.GetByArtistAsync(artistId, cancellationToken);
    }
}