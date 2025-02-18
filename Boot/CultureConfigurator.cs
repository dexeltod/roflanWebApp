using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace Boot;

public class CultureConfigurator(WebApplication app)
{
	public void Configure()
	{
		var supportedCultures = new[]
		{
			new CultureInfo("en-US"),
			new CultureInfo("ru-RU")
		};

		app.UseRequestLocalization(
			new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture("en-US"),
				SupportedCultures = supportedCultures,
				SupportedUICultures = supportedCultures
			}
		);
	}
}