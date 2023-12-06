using Microsoft.EntityFrameworkCore;
using MusicCatalog.Application.Shared;
using MusicCatalog.Domain;
using MusicCatalog.Domain.Models;
using MusicCatalog.EfCorePersistence.Data;

namespace MusicCatalog.EfCorePersistence.Repositories;

public abstract class CrudRepository<TModel> : ICrudRepository<TModel> where TModel : Model
{
    protected readonly MusicCatalogDbContext DbContext;
    
    protected CrudRepository(MusicCatalogDbContext dbContext)
    {
        DbContext = dbContext;
    }
    
    public async Task<int> AddAsync(TModel model, CancellationToken cancellationToken)
    {
        await DbContext.Set<TModel>().AddAsync(model, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
        return model.Id;
    }

    public async Task UpdateAsync(TModel model, CancellationToken cancellationToken)
    {
        var entity = await DbContext.Set<TModel>().FindAsync(new object?[] { model.Id }, cancellationToken: cancellationToken);
        if (entity is null)
            return;
        
        Update(model, entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await DbContext.Set<TModel>().FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
        if (entity is null)
            return;

        DbContext.Set<TModel>().Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<TModel?> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await DbContext.Set<TModel>().FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public async Task<PaginatedCollection<TModel>> GetAllAsync(FilterPaginationDto dto, CancellationToken cancellationToken)
    {
        var skip = (dto.PageNumber - 1) * dto.PageSize;
        var take = dto.PageSize;

        var query = DbContext.Set<TModel>().AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(dto.SearchTerm))
            query = Filter(query, dto.SearchTerm);
        
        var totalItems = await query.CountAsync(cancellationToken);
        
        var orderBy = string.IsNullOrWhiteSpace(dto.SortColumn) ? "Id" : dto.SortColumn;
        query = Sort(query, orderBy, dto.SortOrder == SortOrder.Asc);
        
        var models = await query
            .Skip(skip)
            .Take(take)
            .ToArrayAsync(cancellationToken);
        
        return new PaginatedCollection<TModel>(models, totalItems);
    }
    
    protected abstract void Update(TModel model, TModel entity);
    
    protected abstract IQueryable<TModel> Filter(IQueryable<TModel> query, string filter);
    
    protected abstract IQueryable<TModel> Sort(IQueryable<TModel> query, string orderBy, bool isAscending);

    protected virtual IQueryable<TModel> Include(IQueryable<TModel> query)
    {
        return query;
    }
}
