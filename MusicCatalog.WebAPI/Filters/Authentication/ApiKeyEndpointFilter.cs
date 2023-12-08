using MusicCatalog.WebAPI.Authentication;

namespace MusicCatalog.WebAPI.Filters.Authentication;

public class ApiKeyEndpointFilter: IEndpointFilter
{
    private readonly IConfiguration _configuration;

    public ApiKeyEndpointFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context, 
        EndpointFilterDelegate next)
    {
        if(!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.KeyHeaderName, out var extractedApiKey))
        {
            return Results.Unauthorized();
        }
        
        var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
        
        if(!apiKey!.Equals(extractedApiKey))
        {
            return Results.Unauthorized();
        }
        return await next(context);
    }
}