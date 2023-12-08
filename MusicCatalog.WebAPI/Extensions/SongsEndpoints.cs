using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.Application.Songs;
using MusicCatalog.Domain;
using MusicCatalog.WebAPI.Filters.Authentication;

namespace MusicCatalog.WebAPI.Extensions;

public static class SongsEndpoints
{
    public static void AddSongsEndpoints(this RouteGroupBuilder appGroup)
    {
        appGroup.MapGet("/", GetAllAsync);
        appGroup.MapGet("/{id:int}", GetAsync).AddEndpointFilter<ApiKeyEndpointFilter>();;
    }
    
    private static async Task<IResult> GetAllAsync(ISongService songService, CancellationToken cancellationToken)
    {
        var songs = await songService.GetAllAsync(new FilterPaginationDto(), cancellationToken);
        return Results.Ok(songs.Models);
    }
    private static async Task<IResult> GetAsync(int id, ISongService songService, CancellationToken cancellationToken)
    {
        var song = await songService.GetAsync(id, cancellationToken);
        return song is null ? Results.NotFound() : Results.Ok(song);
    }
}