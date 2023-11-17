using Microsoft.AspNetCore.Mvc.Rendering;
using MusicCatalog.Domain.Models;
using MusicCatalog.MVCView.Controllers;
using MusicCatalog.MVCView.ViewModels.Shared;

namespace MusicCatalog.MVCView.ViewModels.Artists;

public class ArtistsViewModel : PaginatedFilteredViewModel
{
    
    public ArtistsViewModel() : base(typeof(Artist), typeof(ArtistsController))
    {
    }

    public override ModalViewModel GetModal(Model model, string column)
    {
        const string header = "Songs";
        var artist = (Artist)model;
        return column switch
        {
            nameof(Artist.Songs) => new ArtistsSongsModalViewModel(
                artist.Id,
                header,
                artist.Songs ?? Enumerable.Empty<Song>()),
            _ => throw new ArgumentException($"Column '{column}' has no modal.")
        };
    }

    public override ModalViewModel GetUpdateModal(Model model)
    {
        const string header = "Update artist";
        var artist = (Artist)model;
        return new ArtistsUpdateModalViewModel(artist.Id, header, artist);
    }
    
    public IEnumerable<ModalViewModel> GetModals()
    {
        const string header = "Songs";
        var artists = Models.Cast<Artist>();
        foreach (var artist in artists)
        {
            yield return new ArtistsSongsModalViewModel(
                artist.Id,
                header,
                artist.Songs ?? Enumerable.Empty<Song>());
        }
    }
}