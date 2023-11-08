using Microsoft.EntityFrameworkCore;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.EfCorePersistence.Data;

public class MusicCatalogDbContext: DbContext
{
    public DbSet<Artist> Artists { get; set; } = null!;
    public DbSet<MusicGenre> Genres { get; set; } = null!;
    public DbSet<Song> Songs { get; set; } = null!;
    
    public MusicCatalogDbContext(DbContextOptions<MusicCatalogDbContext> options) : base(options)
    {
        
    }
    
}