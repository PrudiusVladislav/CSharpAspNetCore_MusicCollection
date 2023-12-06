using System.Collections;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.Application.Shared;

public sealed record PaginatedCollection<TModel>(IReadOnlyCollection<TModel> Models, int Total) : IEnumerable<TModel> where TModel : Model
{
    public IEnumerator<TModel> GetEnumerator() => Models.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}