using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicCatalog.Application.MusicGenres;

namespace MusicCatalog.RazorPagesView.Pages.Genres;

public class CreateGenre : PageModel
{
    private readonly IGenreService _genreService;

    public CreateGenre(IGenreService genreService)
    {
        _genreService = genreService;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(string name, CancellationToken cancellationToken)
    {
        await _genreService.CreateAsync(new Domain.Models.MusicGenre() { Name = name }, cancellationToken);
        return Redirect("/Genres/GenresIndex");
    }

}