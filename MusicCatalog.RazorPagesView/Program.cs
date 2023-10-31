using MusicCatalog.Application.Artists;
using MusicCatalog.Application.MusicGenres;
using MusicCatalog.Application.Songs;
using MusicCatalog.MemoryPersistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    
    options.Conventions.AddPageRoute("/Songs/SongsIndex", "/Songs");
    options.Conventions.AddPageRoute("/Genres/GenresIndex", "/Genres");
    options.Conventions.AddPageRoute("/Artists/ArtistsIndex", "/Artists");
    options.Conventions.AddPageRoute("/Songs/UpdateSong", "/Songs/Update");
    options.Conventions.AddPageRoute("/Songs/CreateSong", "/Songs/Create");
    options.Conventions.AddPageRoute("/Genres/UpdateGenre", "/Genres/Update");
    options.Conventions.AddPageRoute("/Artists/UpdateArtist", "/Artists/Update");
    options.Conventions.AddPageRoute("/Artists/CreateArtist", "/Artists/Create");
});

builder.Services.AddTransient<IGenreRepository, GenreRepository>();
builder.Services.AddTransient<IArtistRepository, ArtistRepository>();
builder.Services.AddTransient<ISongRepository, SongRepository>();

builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<IArtistService, ArtistService>();
builder.Services.AddTransient<ISongService, SongService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();