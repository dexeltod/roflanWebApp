namespace Presentation.Middlewares;

public static class CultureExtensions
{
    public static IApplicationBuilder UseCulture(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CultureMiddleware>();
    }
}