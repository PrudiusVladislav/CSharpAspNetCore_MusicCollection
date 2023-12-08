using MusicCatalog.WebAPI.Authentication;

namespace MusicCatalog.WebAPI.Middleware;

public class AuthApiKeyMiddleware: IMiddleware
{
    private readonly IConfiguration _configuration;
    
    public AuthApiKeyMiddleware(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if(!context.Request.Headers.TryGetValue(AuthConstants.KeyHeaderName, out var extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Api Key was not provided. (Unauthorized)");
            return;
        }
        
        var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
        
        if(!apiKey!.Equals(extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized Api Key. (Unauthorized)");
            return;
        }
        await next(context);
    }
}