using MusicCatalog.Domain.Models;
using MusicCatalog.MVCView.ViewModels.Shared;

namespace MusicCatalog.MVCView.ViewModels.Genres;

public class GenresCreateModalViewModel: ModalViewModel
{
    private const string PartialName = "~/Views/Genres/_Create.cshtml";

    private readonly int _genreId;
    
    public override string Id => "view-genre-create";
    public override string HeaderId => "view-genre-create-header";
    public override string ControllerName => "Genres";
    public override string ActionName => "Create";
    
    public GenresCreateModalViewModel(
        string header, 
        MusicGenre genre) : base(header, PartialName, genre)
    {
    }

    public override IDictionary<string, string> GetRouteValues()
    {
        var genre = (MusicGenre)Data;
        return new Dictionary<string, string>()
        {
            { "Name", genre.Name }
        };
    }
}