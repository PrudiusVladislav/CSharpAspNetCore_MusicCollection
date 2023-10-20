using MusicCatalog.Domain;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.Application.MusicCategories;

public interface ICategoryRepository
{
    Task<int> AddAsync(MusicGenre song, CancellationToken cancellationToken);
    Task UpdateAsync(MusicGenre song, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
    Task<MusicGenre> GetAsync(int id, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<MusicGenre>> GetAllAsync(FilterPaginationDto dto, CancellationToken cancellationToken);
}