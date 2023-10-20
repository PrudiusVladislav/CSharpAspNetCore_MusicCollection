using MusicCatalog.Domain;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.Application.Shared;


public interface ICrudRepository<TModel> where TModel : Model
{
    Task<int> AddAsync(TModel model, CancellationToken cancellationToken);
    Task UpdateAsync(TModel model, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
    Task<TModel?> GetAsync(int id, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<TModel>> GetAllAsync(FilterPaginationDto dto, CancellationToken cancellationToken);
}
