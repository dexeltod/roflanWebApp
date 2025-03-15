using Boot.Extensions.WebApplicationExtensions;
using Boot.Middlewares;

namespace Boot;

public class Program
{
	public static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		new DependencyInjection(
			builder.Services,
			builder.Configuration
		).AddServices();

		WebApplication app = builder.Build();
		new CultureConfigurator(app).Configure();

		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			app.UseHsts();
		}
		else
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseCors("AllowReactApp");
		app.UseRouting();
		app.UseStaticFiles();
		app.UseAuthentication();
		app.UseAuthorization();
		app.MapControllerRoute("default", "{controller=UserRegistration}/{action=Index}/{id?}");

		app.MapMinimalApi();

		app.UseMiddleware<CultureMiddleware>();
		app.Run();
	}
}
