using MusicCatalog.Domain;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.Application.MusicCategories;

public interface ICategoryService
{
    Task<int> CreateAsync(MusicGenre genre,
        CancellationToken cancellationToken);
    Task UpdateAsync(MusicGenre genre,
        CancellationToken cancellationToken);
    Task DeleteAsync(int id,
        CancellationToken cancellationToken);
    Task<MusicGenre> GetAsync(int id,
        CancellationToken cancellationToken);
    Task<IReadOnlyCollection<MusicGenre>> GetAllAsync(FilterPaginationDto dto,
        CancellationToken cancellationToken);
}