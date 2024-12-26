using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Presentation;
using Presentation.Factories;
using Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

new DependencyInjection(builder.Services).AddServices();

var app = builder.Build();

new CultureConfigurator(app).Configure();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();

app.UseRouting();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}"
);

app.UseMiddleware<CultureMiddleware>();

app.Run();

namespace Presentation
{
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
                new RequestLocalizationOptions()
                {
                    DefaultRequestCulture = new RequestCulture("en-US"),
                    SupportedCultures = supportedCultures,
                    SupportedUICultures = supportedCultures
                }
            );
        }
    }
}