using MusicCatalog.Domain.Models;
using MusicCatalog.MVCView.ViewModels.Shared;

namespace MusicCatalog.MVCView.ViewModels.Genres;

public class GenresSongsModalViewModel: ModalViewModel
{
    private const string PartialName = "~/Views/Genres/_SongsInfo.cshtml";

    private readonly int _genreId;
    
    public override string Id => $"view-songs-details-{_genreId}";
    public override string HeaderId => $"view-songs-details-{_genreId}-header";
    public override string ControllerName => "Genres";
    public override string ActionName => "Index";
    
    public GenresSongsModalViewModel(
        int genreId,
        string header, 
        IEnumerable<Song> songs) : base(header, PartialName, songs, true)
    {
        _genreId = genreId;
    }

}