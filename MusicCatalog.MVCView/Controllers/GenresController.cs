using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MusicCatalog.Application.MusicGenres;
using MusicCatalog.Application.Shared;
using MusicCatalog.Domain;
using MusicCatalog.Domain.Models;
using MusicCatalog.MVCView.Controllers.Shared;
using MusicCatalog.MVCView.Dtos.Shared;
using MusicCatalog.MVCView.ViewModels.Genres;

namespace MusicCatalog.MVCView.Controllers;

public class GenresController : PaginatedFilteredController<MusicGenre, PaginatedFilteredDto, GenresViewModel>
{
    private readonly IGenreService _genreService;
    
    public GenresController(IGenreService crudService, IMemoryCache memoryCache) : base(crudService, memoryCache)
    {
        _genreService = crudService;
    }
}