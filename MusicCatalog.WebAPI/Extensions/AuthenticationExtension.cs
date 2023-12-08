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
}