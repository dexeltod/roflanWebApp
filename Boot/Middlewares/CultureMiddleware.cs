using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace Boot.Middlewares;

public class CultureMiddleware
{
	private readonly RequestDelegate _next;

	public CultureMiddleware(RequestDelegate next) => _next = next;

	public async Task Invoke(HttpContext context)
	{
		string? lang = context.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.Name;

		if (!string.IsNullOrEmpty(lang))
			try
			{
				CultureInfo.CurrentCulture = new CultureInfo(lang);
				CultureInfo.CurrentUICulture = new CultureInfo(lang);
			}
			catch (CultureNotFoundException)
			{
			}

		await _next.Invoke(context);
	}
}