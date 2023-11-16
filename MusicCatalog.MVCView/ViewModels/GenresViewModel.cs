using Microsoft.AspNetCore.Mvc.Rendering;
using MusicCatalog.Domain.Models;
using MusicCatalog.MVCView.Controllers;
using MusicCatalog.MVCView.ViewModels.Shared;

namespace MusicCatalog.MVCView.ViewModels;

public class GenresViewModel: PaginatedFilteredViewModel
{
    private static readonly IReadOnlyCollection<string> ColumnNames = new[]
    {
        nameof(MusicGenre.Name)
    };
    
    private static readonly IReadOnlyCollection<SelectListItem> InitialSortColumns = new[]
    {
        new SelectListItem("", "Id", true),
        new SelectListItem("Name", "Name")
    };
    
    public override IEnumerable<string> Columns => ColumnNames;
    public override IEnumerable<SelectListItem> SortColumns => InitialSortColumns.ToList();

    public GenresViewModel() : base(typeof(MusicGenre), typeof(GenresController))
    {
    }
    
    public override string GetColumnValue(Model model, string column)
    {
        if (model is not MusicGenre genre)
            throw new ArgumentException($"Model '{model.GetType()}' is not a category.");
        
        return column switch
        {
            nameof(MusicGenre.Id) => genre.Id.ToString(),
            nameof(MusicGenre.Name) => genre.Name,
            _ => throw new ArgumentException($"Column '{column}' is not supported.")
        };
    }

}