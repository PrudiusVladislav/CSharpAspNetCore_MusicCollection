using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using MusicCatalog.Application.Shared;
using MusicCatalog.Domain;
using MusicCatalog.Domain.Models;
using MusicCatalog.MVCView.Dtos.Shared;
using MusicCatalog.MVCView.Extensions;
using MusicCatalog.MVCView.ViewModels;
using MusicCatalog.MVCView.ViewModels.Shared;

namespace MusicCatalog.MVCView.Controllers.Shared;

public abstract class PaginatedFilteredController<TModel, TPaginatedFilteredDto, TViewModel> : Controller 
    where TModel : Model
    where TPaginatedFilteredDto : PaginatedFilteredDto
    where TViewModel : PaginatedFilteredViewModel, new()
{
    //private static readonly  IDictionary<int, PaginatedCollection<TModel>> CachedModels = new Dictionary<int, PaginatedCollection<TModel>>();
    private readonly IMemoryCache _memoryCache;
    private static DateTime _timeOfLastDbUpdate = DateTime.Now;
    private readonly ICrudService<TModel> _crudService;
    
    protected PaginatedFilteredController(ICrudService<TModel> crudService, IMemoryCache memoryCache)
    {
        _crudService = crudService;
        _memoryCache = memoryCache;
    }
    
    public virtual IActionResult Index(TPaginatedFilteredDto dto)
    {
        (dto.SortColumn, dto.SortDirection) = PaginatedFilteredViewModel.GetSortColumnAndDirection(dto.Sorting, dto.SortColumn, dto.IsSortChanged);
        
        if (dto.Page <= 0)
            dto.Page = 1;

        return RedirectToAction("Filter", dto);
    }

    public virtual async Task<IActionResult> Filter(TPaginatedFilteredDto dto,
        CancellationToken cancellationToken = default)
    {
        var sortOrder = dto.SortDirection.Equals("Descending") ? SortOrder.Desc : SortOrder.Asc;

        var domainDto = new FilterPaginationDto(dto.SearchTerm, dto.Page, dto.PageSize, dto.SortColumn, sortOrder);
        var compositeKey = domainDto.GetCompositeKey(_timeOfLastDbUpdate);
        if (!_memoryCache.TryGetValue(compositeKey, out PaginatedCollection<TModel>? models))
        {
            models = await _crudService.GetAllAsync(domainDto, cancellationToken);
            //await Task.Delay(3000, cancellationToken);
            _memoryCache.Set(compositeKey, models, new MemoryCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromSeconds(30),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });
        }

        var viewModel = new TViewModel()
        {
            Total = models!.Total,
            SortColumn = dto.SortColumn,
            SortDirection = dto.SortDirection,
            CurrentPage = dto.Page,
            PageSize = dto.PageSize,
            SearchTerm = dto.SearchTerm
        };
        viewModel.AddModels(models.Models);
        return View("Index", viewModel);
    }
    
    [HttpPost]
    public virtual async Task<IActionResult> Create(TModel model, CancellationToken cancellationToken)
    {
        await _crudService.CreateAsync(model, cancellationToken);
        _timeOfLastDbUpdate = DateTime.Now;
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    public virtual async Task<IActionResult> Edit(TModel model, CancellationToken cancellationToken)
    {
        await _crudService.UpdateAsync(model, cancellationToken);
        _timeOfLastDbUpdate = DateTime.Now;
        return RedirectToAction(nameof(Index));
    }
    
    public virtual async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _crudService.DeleteAsync(id, cancellationToken);
        _timeOfLastDbUpdate = DateTime.Now;
        return RedirectToAction(nameof(Index));
    }

}