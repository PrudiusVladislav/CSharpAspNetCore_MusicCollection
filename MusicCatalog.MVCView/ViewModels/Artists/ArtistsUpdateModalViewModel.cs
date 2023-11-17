using MusicCatalog.Domain.Models;
using MusicCatalog.MVCView.ViewModels.Shared;

namespace MusicCatalog.MVCView.ViewModels.Artists;

public class ArtistsUpdateModalViewModel: ModalViewModel
{
    private const string PartialName = "~/Views/Artists/_Update.cshtml";

    private readonly int _artistId;
    
    public override string Id => $"view-artist-update-{_artistId}";
    public override string HeaderId => $"view-artist-update-{_artistId}-header";
    public override string ControllerName => "Artists";
    public override string ActionName => "Edit";
    
    public ArtistsUpdateModalViewModel(
        int artistId,
        string header, 
        Artist artistToUpdate) : base(header, PartialName, artistToUpdate)
    {
        _artistId = artistId;
    }

    public override IDictionary<string, string> GetRouteValues()
    {
        var artist = (Artist)Data;
        return new Dictionary<string, string>()
        {
            { "Id", artist.Id.ToString() },
            { "FirstName", artist.FirstName },
            { "LastName", artist.LastName ?? "" },
            { "DateOfBirth", artist.DateOfBirth.ToShortDateString()}
        };
    }
}