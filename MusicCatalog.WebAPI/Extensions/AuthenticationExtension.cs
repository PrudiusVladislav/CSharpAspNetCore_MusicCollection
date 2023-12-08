using MusicCatalog.WebAPI.Filters.Authentication;
using MusicCatalog.WebAPI.Middleware;

namespace MusicCatalog.WebAPI.Extensions;

public static class AuthenticationExtension
{
    public static void AddApiKeyAuthentication(this IServiceCollection services)
    {
        services.AddTransient<AuthApiKeyMiddleware>();
    }
    
    public static void UseApiKeyAuthentication(this IApplicationBuilder app)
    {
        app.UseMiddleware<AuthApiKeyMiddleware>();
    }
    
    public static void AddApiKeyAuthFilter(this IServiceCollection services)
    {
        services.AddScoped<ApiKeyAuthFilter>();
    }
}