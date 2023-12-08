using Microsoft.AspNetCore.Mvc;
using MusicCatalog.Application.MusicGenres;
using MusicCatalog.Domain;
using MusicCatalog.Domain.Models;
using MusicCatalog.WebAPI.Dtos.Genres;
using MusicCatalog.WebAPI.Filters.Validation;

namespace MusicCatalog.WebAPI.Controllers;

public class GenresController : Controller
{
    private readonly IGenreService _genreService;

    public GenresController(IGenreService genreService, ILogger<GenresController> logger)
        : base(logger)
    {
        _genreService = genreService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        var categories = await _genreService.GetAllAsync(paginationDto, cancellationToken);
        return Ok(categories);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var category = await _genreService.GetAsync(id, cancellationToken);
        if (category is null)
            return NotFound();

        return Ok(category);
    }

    [HttpPost]
    [ValidationFilter]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateGenreDto dto, CancellationToken cancellationToken)
    {
        var genre = new MusicGenre() { Name = dto.Name };
        var id = await _genreService.CreateAsync(genre, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateGenreDto dto, CancellationToken cancellationToken)
    {
        var genre = new MusicGenre() { Id = dto.Id, Name = dto.Name };
        await _genreService.UpdateAsync(genre, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _genreService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

}