using Microsoft.AspNetCore.Mvc.Rendering;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.MVCView.ViewModels;

public class ArtistsViewModel
{
    public IReadOnlyCollection<Artist> Artists { get; set; } = null!;
    public int TotalArtists { get; set; }

    public string SortColumn { get; set; } = null!;
    public string SortDirection { get; set; } = null!;
    
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling(TotalArtists / (decimal)PageSize);

    public string SearchTerm { get; set; } = string.Empty;

    public IReadOnlyCollection<SelectListItem> SortColumns { get; set; } = new[]
    {
        new SelectListItem("", "Id", true),
        new SelectListItem("First Name", "FirstName"),
        new SelectListItem("Last Name", "LastName"),
        new SelectListItem("Date of Birth", "DateOfBirth")
    };
    
    public IReadOnlyCollection<SelectListItem> SortDirections { get; set; } = new[]
    {
        new SelectListItem("Ascending", "Ascending"),
        new SelectListItem("Descending", "Descending", true)
    };

    public IDictionary<string, string> ToDictionaryParameters()
    {
        return new Dictionary<string, string>
        {
            { nameof(SearchTerm), SearchTerm },
            { nameof(SortColumn), SortColumn },
            { nameof(SortDirection), SortDirection },
            { nameof(PageSize), PageSize.ToString() },
            { nameof(CurrentPage), CurrentPage.ToString() }
        };
    }

}