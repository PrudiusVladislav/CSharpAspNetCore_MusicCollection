using Microsoft.AspNetCore.Mvc.Filters;

namespace MusicCatalog.WebAPI.Extensions;

public static class ValidationFilterExtensions
{
    public static FilterCollection AddValidationFilter(this FilterCollection filters)
    {
        filters.Add<ValidationFeatureFilter>();
        return filters;
    }
}