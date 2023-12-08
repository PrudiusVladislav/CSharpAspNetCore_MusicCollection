using MusicCatalog.WebAPI.Middleware;

namespace MusicCatalog.WebAPI.Extensions;

public static class LoggingMiddlewareExtension
{
    public static void AddLoggingMiddleware(this IServiceCollection services)
    {
        services.AddTransient<LoggingMiddleware>();
    }
    
    public static void UseLoggingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<LoggingMiddleware>();
    }
}