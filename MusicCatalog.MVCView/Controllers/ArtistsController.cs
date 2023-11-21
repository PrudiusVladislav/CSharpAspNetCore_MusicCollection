using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MusicCatalog.Application.Artists;
using MusicCatalog.Domain;
using MusicCatalog.Domain.Models;
using MusicCatalog.MVCView.Controllers.Shared;
using MusicCatalog.MVCView.Dtos.Shared;
using MusicCatalog.MVCView.ViewModels.Artists;

namespace MusicCatalog.MVCView.Controllers;

public class ArtistsController : PaginatedFilteredController<Artist, PaginatedFilteredDto, ArtistsViewModel>
{
    private readonly IArtistService _genreService;
    
    public ArtistsController(IArtistService crudService, IMemoryCache memoryCache) : base(crudService, memoryCache)
    {
        _genreService = crudService;
    }
}