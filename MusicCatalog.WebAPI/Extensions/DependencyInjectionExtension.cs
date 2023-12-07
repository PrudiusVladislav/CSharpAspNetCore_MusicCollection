using Microsoft.EntityFrameworkCore;
using MusicCatalog.Application.Artists;
using MusicCatalog.Application.MusicGenres;
using MusicCatalog.Application.Songs;
using MusicCatalog.EfCorePersistence.Data;
using MusicCatalog.EfCorePersistence.Repositories;

namespace MusicCatalog.WebAPI.Extensions;

public static class DependencyInjectionExtension
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlClient");
        services.AddDbContext<MusicCatalogDbContext>(options => 
            options.UseSqlServer(connectionString, x => x.MigrationsAssembly("MusicCatalog.EfCorePersistence")));
        
        services.AddTransient<IGenreRepository, GenresRepository>();
        services.AddTransient<IArtistRepository, ArtistsRepository>();
        services.AddTransient<ISongRepository, SongsRepository>();

        services.AddTransient<IGenreService, GenreService>();
        services.AddTransient<IArtistService, ArtistService>();
        services.AddTransient<ISongService, SongService>();
    }
}