using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicCatalog.Application.MusicGenres;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.RazorPagesView.Pages.Genres;

public class UpdateGenre : PageModel
{
    private readonly IGenreService _genreService;

    public UpdateGenre(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [BindProperty]
    public MusicGenre Genre { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        var genre = await _genreService.GetAsync(id, cancellationToken);
        if (genre == null)
            return NotFound();

        Genre = genre;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string name, CancellationToken cancellationToken)
    {
        await _genreService.UpdateAsync(new MusicGenre() { Id = Genre.Id, Name = name }, cancellationToken);
        return Redirect("/Genres/GenresIndex");
    }

}