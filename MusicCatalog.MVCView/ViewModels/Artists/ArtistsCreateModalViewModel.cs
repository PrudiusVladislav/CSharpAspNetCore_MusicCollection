using MusicCatalog.Domain.Models;
using MusicCatalog.MVCView.ViewModels.Shared;

namespace MusicCatalog.MVCView.ViewModels.Artists;

public class ArtistsCreateModalViewModel : ModalViewModel
{
    private const string PartialName = "~/Views/Artists/_Create.cshtml";

    private readonly int _genreId;
    
    public override string Id => "view-arist-create";
    public override string HeaderId => "view-artist-create-header";
    public override string ControllerName => "Artists";
    public override string ActionName => "Create";
    
    public ArtistsCreateModalViewModel(
        string header, 
        Artist artist) : base(header, PartialName, artist)
    {
    }

    public override IDictionary<string, string> GetRouteValues()
    {
        var artist = (Artist)Data;
        return new Dictionary<string, string>()
        {
            { "FirstName", artist.FirstName },
            { "LastName", artist.LastName ?? "" },
            { "DateOfBirth", artist.DateOfBirth.ToShortDateString()}
        };
    }
}