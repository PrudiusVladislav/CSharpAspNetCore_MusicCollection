using MusicCatalog.Domain.Models;
using MusicCatalog.MVCView.ViewModels.Shared;

namespace MusicCatalog.MVCView.ViewModels.Artists;

public class ArtistsSongsModalViewModel: ModalViewModel
{
    private const string PartialName = "~/Views/Artists/_SongsInfo.cshtml";

    private readonly int _genreId;
    
    public override string Id => $"view-songs-details-{_genreId}";
    public override string HeaderId => $"view-songs-details-{_genreId}-header";
    public override string ControllerName => "Artists";
    public override string ActionName => "Index";
    
    public ArtistsSongsModalViewModel(
        int genreId,
        string header, 
        IEnumerable<Song> songs) : base(header, PartialName, songs)
    {
        _genreId = genreId;
    }
}