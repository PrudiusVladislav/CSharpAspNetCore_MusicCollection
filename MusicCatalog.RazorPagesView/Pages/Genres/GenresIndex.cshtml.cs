using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicCatalog.Application.MusicGenres;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.RazorPagesView.Pages.Genres;

public class GenresIndex : PageModel
{
    private int _pageNumber = 1;

    private readonly IGenreService _genreService;

    public IReadOnlyCollection<MusicGenre> Genres { get; set; } = new List<MusicGenre>();

    public GenresIndex(IGenreService genreService)
    {
        _genreService = genreService;
    }

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Genres = await _genreService.GetAllAsync(new Domain.FilterPaginationDto(string.Empty, _pageNumber), cancellationToken);
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _genreService.DeleteAsync(id, cancellationToken);
        return RedirectToPage();
    }

}