using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicCatalog.Application.Artists;
using MusicCatalog.Application.MusicGenres;
using MusicCatalog.Application.Songs;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.RazorPagesView.Pages.Songs;

public class UpdateSong : PageModel
{
    private readonly ISongService _songService;
    private readonly IArtistService _artistService;
    private readonly IGenreService _genreService;

    [BindProperty]
    public Song Song { get; set; } = null!;
    
    [BindProperty]
    public int GenreId { get; set; }
    
    [BindProperty]
    public List<int> ArtistIds { get; set; } = new List<int>();
        

    public IReadOnlyCollection<SelectListItem> Genres { get; set; } = new List<SelectListItem>();
    public IReadOnlyCollection<SelectListItem> Artists { get; set; } = new List<SelectListItem>();

    public UpdateSong(ISongService songService, IArtistService artistService, IGenreService genreService)
    {
        _songService = songService;
        _artistService = artistService;
        _genreService = genreService;
    }    

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        var song = await _songService.GetAsync(id, cancellationToken);
        if (song is null)
            return NotFound();

        Song = song;

        GenreId = song.GenreId ?? 0;
        ArtistIds = song.Artists?.Select(a => a.Id).ToList() ?? new List<int>();

        var genres = await _genreService.GetAllAsync(new Domain.FilterPaginationDto(string.Empty), cancellationToken);
        var artists = await _artistService.GetAllAsync(new Domain.FilterPaginationDto(string.Empty), cancellationToken);

        Genres = genres.Select(c => new SelectListItem(c.Name, c.Id.ToString(), 
            song.GenreId == c.Id)).ToList();
        Artists = artists.Select(a => new SelectListItem(a.ToString(), a.Id.ToString(),
            song.Artists?.Select(a => a.Id).Contains(a.Id) is true)).ToList();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string title, string duration, DateTime releaseDate, CancellationToken cancellationToken) 
    {
        var genre = GenreId <= 0 ? null : await _genreService.GetAsync(GenreId, cancellationToken);

        var song = new Song() { Id = Song.Id, Title = title,
            Duration = (int.Parse(duration[..duration.IndexOf(':')]) * 60) + int.Parse(duration[(duration.IndexOf(':') + 1)..]),
            ReleaseDate = releaseDate, GenreId = GenreId, Genre = genre };

        var sourceActors = await _artistService.GetAllAsync(new Domain.FilterPaginationDto(string.Empty), cancellationToken);
        song.Artists = sourceActors.Where(a => ArtistIds.Contains(a.Id)).ToList();

        await _songService.UpdateAsync(song, cancellationToken);
        return Redirect("../SongsIndex");
    }   

}