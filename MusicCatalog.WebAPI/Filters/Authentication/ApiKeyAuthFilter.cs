using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MusicCatalog.WebAPI.Authentication;

namespace MusicCatalog.WebAPI.Filters.Authentication;

public class ApiKeyAuthFilter: IAuthorizationFilter
{
    private readonly IConfiguration _configuration;

    public ApiKeyAuthFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if(!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.KeyHeaderName, out var extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult("Api Key was not provided. (Unauthorized)");
            return;
        }
        
        var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);

        if (!apiKey!.Equals(extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult("Unauthorized Api Key. (Unauthorized)");
        }
    }
}