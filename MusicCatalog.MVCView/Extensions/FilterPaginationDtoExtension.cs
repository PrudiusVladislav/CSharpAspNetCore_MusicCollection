using MusicCatalog.Domain;

namespace MusicCatalog.MVCView.Extensions;

public static class FilterPaginationDtoExtension
{
    public static string GetCompositeKey(this FilterPaginationDto dto, DateTime timeOfCurrentDbVersion)
    {
        return $"{dto.SearchTerm}_{dto.PageNumber}_{dto.PageSize}_{dto.SortColumn}_{dto.SortOrder}_{timeOfCurrentDbVersion}";
    }
}