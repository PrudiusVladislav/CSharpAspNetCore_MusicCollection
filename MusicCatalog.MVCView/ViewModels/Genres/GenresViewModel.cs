using Microsoft.AspNetCore.Mvc.Rendering;
using MusicCatalog.Domain.Models;
using MusicCatalog.MVCView.Controllers;
using MusicCatalog.MVCView.ViewModels.Genres;
using MusicCatalog.MVCView.ViewModels.Shared;

namespace MusicCatalog.MVCView.ViewModels.Genres;

public class GenresViewModel: PaginatedFilteredViewModel
{
    public GenresViewModel() : base(typeof(MusicGenre), typeof(GenresController))
    {
    }

    public override ModalViewModel GetModal(Model model, string column)
    {
        const string header = "Songs";
        var genre = (MusicGenre)model;
        return column switch
        {
            nameof(MusicGenre.Songs) => new GenresSongsModalViewModel(
                genre.Id,
                header,
                genre.Songs ?? Enumerable.Empty<Song>()),
            _ => throw new ArgumentException($"Column '{column}' has no modal.")
        };
    }

    public override ModalViewModel GetUpdateModal(Model model)
    {
        const string header = "Update genre";
        var genre = (MusicGenre)model;
        return new GenreUpdateModalViewModel(genre.Id, header, genre);
    }
    
    // public override ModalViewModel GetCreateModal()
    // {
    //     const string header = "Update genre";
    //     return new GenreCreateModalViewModel(genre.Id, header, genre);
    // }
    
    public IEnumerable<ModalViewModel> GetModals()
    {
        const string header = "Songs";
        var genres = Models.Cast<MusicGenre>();
        foreach (var genre in genres)
        {
            yield return new GenresSongsModalViewModel(
                genre.Id,
                header,
                genre.Songs ?? Enumerable.Empty<Song>());
        }
    }

}