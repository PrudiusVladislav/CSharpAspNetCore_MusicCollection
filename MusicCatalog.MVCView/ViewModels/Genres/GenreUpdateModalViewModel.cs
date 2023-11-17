using MusicCatalog.Domain.Models;
using MusicCatalog.MVCView.ViewModels.Shared;

namespace MusicCatalog.MVCView.ViewModels.Genres;

public class GenreUpdateModalViewModel: ModalViewModel
{
    private const string PartialName = "~/Views/Genres/_Update.cshtml";

    private readonly int _genreId;
    
    public override string Id => $"view-genre-update-{_genreId}";
    public override string HeaderId => $"view-genre-update-{_genreId}-header";
    public override string ControllerName => "Genres";
    public override string ActionName => "Edit";
    
    public GenreUpdateModalViewModel(
        int genreId,
        string header, 
        MusicGenre genreToUpdate) : base(header, PartialName, genreToUpdate)
    {
        _genreId = genreId;
    }

    public override IDictionary<string, string> GetRouteValues()
    {
        var genre = (MusicGenre)Data;
        return new Dictionary<string, string>()
        {
            { "Id", genre.Id.ToString() },
            { "Name", genre.Name }
        };
    }
}