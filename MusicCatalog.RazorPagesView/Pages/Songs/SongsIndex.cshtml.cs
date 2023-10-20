using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicCatalog.Application.Songs;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.RazorPagesView.Pages.Songs;

public class SongsIndex : PageModel
{
    private readonly ISongService _songService;

    public IReadOnlyCollection<Song> Songs { get; set; } = new List<Song>();

    public SongsIndex(ISongService songService)
    {
        _songService = songService;
    }

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Songs = await _songService.GetAllAsync(new Domain.FilterPaginationDto(string.Empty), cancellationToken);
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _songService.DeleteAsync(id, cancellationToken);
        return RedirectToPage();
    }

}