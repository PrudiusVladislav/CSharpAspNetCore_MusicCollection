using MusicCatalog.Domain;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.Application.Shared;

public abstract class CrudService<TModel> : ICrudService<TModel> where TModel : Model
{
    private readonly ICrudRepository<TModel> _repository;

    public CrudService(ICrudRepository<TModel> repository)
    {
        _repository = repository;
    }

    public virtual async Task<int> CreateAsync(TModel model, CancellationToken cancellationToken)
    {
        return await _repository.AddAsync(model, cancellationToken);
    }

    public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(id, cancellationToken);
    }

    public virtual async  Task<IReadOnlyCollection<TModel>> GetAllAsync(FilterPaginationDto dto, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(dto, cancellationToken);
    }

    public virtual async Task<TModel?> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await _repository.GetAsync(id, cancellationToken);
    }

    public virtual async Task UpdateAsync(TModel model, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(model, cancellationToken);
    }
}

