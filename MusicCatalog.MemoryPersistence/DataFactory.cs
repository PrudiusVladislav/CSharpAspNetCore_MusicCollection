using MusicCatalog.Domain.Models;

namespace MusicCatalog.MemoryPersistence;

internal sealed class DataFactory
{
	private static DataFactory? _instance;
	public static DataFactory Instance => _instance ??= new DataFactory(true);

	private readonly IReadOnlyCollection<Artist> _actors;
	private readonly IReadOnlyCollection<MusicGenre> _categories;
	private readonly IReadOnlyCollection<Song> _movies;

	public DataFactory(bool seedData = false)
	{
		_actors = new List<Artist>();
		_categories = new List<MusicGenre>();
		_movies = new List<Song>();

		if (!seedData)
			return;

		(_actors, _categories, _movies) = CreateData();
	}

	public IReadOnlyCollection<TModel> GetModels<TModel>()
	{
		if (typeof(TModel) == typeof(Artist))
			return (IReadOnlyCollection<TModel>)_actors;
		if (typeof(TModel) == typeof(MusicGenre))
			return (IReadOnlyCollection<TModel>)_categories;
		if (typeof(TModel) == typeof(Song))
			return (IReadOnlyCollection<TModel>)_movies;
		return Array.Empty<TModel>();
	}

	private static (IReadOnlyCollection<Artist> Artists, IReadOnlyCollection<MusicGenre> Genres, IReadOnlyCollection<Song> Songs) CreateData()
	{
		var artists = new List<Artist>
		{
			new() { Id = 1, FirstName = "John", LastName = "Lennon", DateOfBirth = new DateTime(1940, 10, 9) },
			new() { Id = 2, FirstName = "Paul", LastName = "McCartney", DateOfBirth = new DateTime(1942, 6, 18) },
			new() { Id = 3, FirstName = "George", LastName = "Harrison", DateOfBirth = new DateTime(1943, 2, 25) },
			new() { Id = 4, FirstName = "Ringo", LastName = "Starr", DateOfBirth = new DateTime(1940, 7, 7) },
		};

		var genres = new List<MusicGenre>
		{
			new() { Id = 1, Name = "Rock" },
			new() { Id = 2, Name = "Pop" },
			new() { Id = 3, Name = "Classic" },
		};

		var songs = new List<Song>
		{
			new() { Id = 1, Title = "Imagine", Duration = 185, GenreId = 1, Genre = genres[0], ReleaseDate = new DateTime(1971, 10, 11), Artists = new List<Artist> { artists[0] } },
			new() { Id = 2, Title = "Let It Be", Duration = 243, GenreId = 2, Genre = genres[1], ReleaseDate = new DateTime(1970, 3, 6), Artists = new List<Artist> { artists[1] } },
			new() { Id = 3, Title = "Here Comes the Sun", Duration = 185, GenreId = 1, Genre = genres[0], ReleaseDate = new DateTime(1969, 7, 26), Artists = new List<Artist> { artists[2] } },
			new() { Id = 4, Title = "Yellow Submarine", Duration = 159, GenreId = 2, Genre = genres[1], ReleaseDate = new DateTime(1966, 8, 5), Artists = new List<Artist> { artists[0], artists[1], artists[3] } },
		};

		return (artists, genres, songs);
	}

	private static IReadOnlyCollection<Artist> CreateActors()
	{
		return new List<Artist>
		{
		};
	}

	private static IReadOnlyCollection<MusicGenre> CreateCategories()
	{
		return new List<MusicGenre>
		{

		};
	}

	private static IReadOnlyCollection<Song> CreateMovies()
	{
		return new List<Song>
		{

		};
	}
}
