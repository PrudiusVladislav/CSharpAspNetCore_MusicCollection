using Microsoft.AspNetCore.Mvc;
using MusicCatalog.Application.Artists;
using MusicCatalog.Domain;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.MVCView.Controllers;

public class ArtistsController : Controller
{
    private readonly IArtistService _artistService;
    
    public ArtistsController(IArtistService artistService)
    {
        _artistService = artistService;
    }
    
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        return View(await _artistService.GetAllAsync(new FilterPaginationDto(), cancellationToken));
    }

    public IActionResult Create()
    {
        return View(new Artist() { FirstName = string.Empty });
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([Bind("FirstName,LastName,DateOfBirth")] Artist artist, CancellationToken cancellationToken)
    {
        await _artistService.CreateAsync(artist, cancellationToken);
        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var artist = await _artistService.GetAsync(id, cancellationToken);
        if (artist is null)
            return NotFound();
        return View(artist);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,DateOfBirth")] Artist artist, CancellationToken cancellationToken)
    {
        if (id != artist.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid) return View(artist);
        
        await _artistService.UpdateAsync(artist, cancellationToken);
        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _artistService.DeleteAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }
}