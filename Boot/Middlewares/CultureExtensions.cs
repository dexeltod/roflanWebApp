namespace Boot.Middlewares;

public static class CultureExtensions
{
    public static IApplicationBuilder UseCulture(this IApplicationBuilder builder) =>
        builder.UseMiddleware<CultureMiddleware>();
}