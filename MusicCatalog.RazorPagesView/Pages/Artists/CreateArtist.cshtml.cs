using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicCatalog.Application.Artists;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.RazorPagesView.Pages.Artists;

public class CreateArtist : PageModel
{
    private readonly IArtistService _artistService;

    public CreateArtist(IArtistService artistService)
    {
        _artistService = artistService;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(string firstName, string lastName, DateTime dateOfBirth, CancellationToken cancellationToken)
    {
        var artist = new Artist() { FirstName = firstName, LastName = lastName, DateOfBirth = dateOfBirth };
        await _artistService.CreateAsync(artist, cancellationToken);
        return Redirect("./ArtistsIndex");
    }

}