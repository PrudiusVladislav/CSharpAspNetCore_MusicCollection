using Microsoft.AspNetCore.Mvc;
using MusicCatalog.Application.MusicGenres;
using MusicCatalog.Application.Shared;
using MusicCatalog.Domain.Models;
using MusicCatalog.MVCView.Controllers.Shared;

namespace MusicCatalog.MVCView.Controllers;

public class GenresController : PaginatedFilteredController<MusicGenre>
{
    private readonly IGenreService _genreService;
    
    public GenresController(IGenreService crudService) : base(crudService)
    {
        _genreService = crudService;
    }
}