using MusicCatalog.Application.Shared;
using MusicCatalog.Domain;
using MusicCatalog.Domain.Models;
using MusicCatalog.MemoryPersistence.Extensions;

namespace MusicCatalog.MemoryPersistence.Repositories;

public abstract class CrudRepository<TModel> : ICrudRepository<TModel> where TModel : Model
{
    protected static readonly IDictionary<int, TModel> Models = DataFactory.Instance.GetModels<TModel>().ToDictionary(m => m.Id);
    protected static int LastId = Models.Max(x => x.Key);

    public virtual Task<int> AddAsync(TModel model, CancellationToken cancellationToken)
    {
        LastId++;
        try
        {
            model.Id = LastId;
            Models.Add(LastId, model);
            return Task.FromResult(LastId);
        }
        catch (Exception)
        {
            LastId--;
            return Task.FromResult(-1);
        }
    }

    public virtual Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            Models.Remove(id);
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            return Task.FromException(ex);
        }
    }

    public virtual Task<PaginatedCollection<TModel>> GetAllAsync(FilterPaginationDto dto, CancellationToken cancellationToken)
    {
        var skip = (dto.PageNumber - 1) * dto.PageSize;
        var take = dto.PageSize;

        var filteredModels = Models.Values
            .Filter(dto.SearchTerm)
            .ToList();
		
        var models = filteredModels
            .SortBy(dto.SortColumn, dto.SortOrder)
            .Skip(skip)
            .Take(take)
            .ToArray();

        var totalItems = filteredModels.Count;
        return Task.FromResult(new PaginatedCollection<TModel>(models, totalItems));

    }

    public virtual Task<TModel?> GetAsync(int id, CancellationToken cancellationToken)
    {
        return Task.FromResult(Models.TryGetValue(id, out var model) ? model : null);
    }

    public virtual Task UpdateAsync(TModel model, CancellationToken cancellationToken)
    {
        if (Models.TryGetValue(model.Id, out var oldModel))
            UpdateModel(oldModel, model);

        return Task.CompletedTask;
    }

    protected abstract void UpdateModel(TModel oldModel, TModel newModel);
}
