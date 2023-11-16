using Microsoft.AspNetCore.Mvc;
using MusicCatalog.Application.Shared;
using MusicCatalog.Domain;
using MusicCatalog.Domain.Models;
using MusicCatalog.MVCView.ViewModels;
using MusicCatalog.MVCView.ViewModels.Shared;

namespace MusicCatalog.MVCView.Controllers.Shared;

public abstract class PaginatedFilteredController<TModel> : Controller where TModel: Model
{
    private readonly ICrudService<TModel> _crudService;
    
    protected PaginatedFilteredController(ICrudService<TModel> crudService)
    {
        _crudService = crudService;
    }
    
    public virtual IActionResult Index(
        string searchTerm = "", 
        string sorting = "",
        string sortColumn = "Id", 
        int page = 1, 
        int pageSize = 10)
    {
        var (sortBy, sortDirection) = PaginatedFilteredViewModel.GetSortColumnAndDirection(sorting, sortColumn);
        
        if (page <= 0)
            page = 1;
        
        return RedirectToAction("Filter", new
        {
            searchTerm,
            sortColumn = sortBy,
            sortDirection,
            page,
            pageSize
        });
    }
    
    public virtual async Task<IActionResult> Filter(
        string searchTerm = "",
        string sortColumn = "",
        string sortDirection = "",
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var sortOrder = sortDirection.Equals("Descending") ? SortOrder.Desc : SortOrder.Asc;
        
        var dto = new FilterPaginationDto(searchTerm, page, pageSize, sortColumn, sortOrder);
        var models = await _crudService.GetAllAsync(dto, cancellationToken);
        var viewModel = new GenresViewModel()
        {
            Total = models.Total,
            SortColumn = sortColumn,
            SortDirection = sortDirection,
            CurrentPage = page,
            PageSize = pageSize,
            SearchTerm = searchTerm
        };
        viewModel.AddModels(models.Models);
        return View("Index", viewModel);
    }

}