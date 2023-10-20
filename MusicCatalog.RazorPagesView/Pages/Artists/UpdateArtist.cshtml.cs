using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicCatalog.Application.Artists;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.RazorPagesView.Pages.Artists;

public class UpdateArtist : PageModel
{
    private readonly IArtistService _artistService;

    [BindProperty]
    public Artist Artist { get; set; } = null!;

    public UpdateArtist(IArtistService artistService)
    {
        _artistService = artistService;
    }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        var artist = await _artistService.GetAsync(id, cancellationToken);
        if (artist is null)
            return NotFound();

        Artist = artist;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string firstName, string lastName, DateTime dateOfBirth, CancellationToken cancellationToken) 
    {
        var artist = new Artist { Id = Artist.Id, FirstName = firstName, LastName = lastName, DateOfBirth = dateOfBirth };
        await _artistService.UpdateAsync(artist, cancellationToken);
        return Redirect("../ArtistsIndex");
    }

}