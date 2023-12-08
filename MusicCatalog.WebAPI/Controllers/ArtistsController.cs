using Microsoft.AspNetCore.Mvc;
using MusicCatalog.Application.Artists;
using MusicCatalog.Domain;
using MusicCatalog.Domain.Models;
using MusicCatalog.WebAPI.Dtos.Artists;
using MusicCatalog.WebAPI.Filters.Validation;

namespace MusicCatalog.WebAPI.Controllers;

public class ArtistsController : Controller
{
    private readonly IArtistService _artistService;

    public ArtistsController(IArtistService artistService, ILogger<ArtistsController> logger)
        : base(logger)
    {
        _artistService = artistService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        var artists = await _artistService.GetAllAsync(paginationDto, cancellationToken);
        return Ok(artists);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var artist = await _artistService.GetAsync(id, cancellationToken);
        if (artist is null)
            return NotFound();

        return Ok(artist);
    }

    [HttpPost]
    [ValidationFilter]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateArtistDto dto, CancellationToken cancellationToken)
    {
        var artist = new Artist() { 
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            DateOfBirth = dto.DateOfBirth,
            Songs = dto.Songs
        };
        var id = await _artistService.CreateAsync(artist, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateArtistDto dto, CancellationToken cancellationToken)
    {
        var artist = new Artist() { 
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            DateOfBirth = dto.DateOfBirth,
            Songs = dto.Songs };
        await _artistService.UpdateAsync(artist, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _artistService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}