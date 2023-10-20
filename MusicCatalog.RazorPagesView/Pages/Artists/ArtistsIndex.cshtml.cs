using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicCatalog.Application.Artists;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.RazorPagesView.Pages.Artists;

public class ArtistsIndex : PageModel
{
    private readonly IArtistService _artistService;

    public IReadOnlyCollection<Artist> Artists { get; set; } = new List<Artist>();

    public ArtistsIndex(IArtistService artistService)
    {
        _artistService = artistService;
    }

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Artists = await _artistService.GetAllAsync(new Domain.FilterPaginationDto(string.Empty), cancellationToken);
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _artistService.DeleteAsync(id, cancellationToken);
        return RedirectToPage();
    }
}