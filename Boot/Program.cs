using Boot;
using Boot.Extensions.WebApplicationExtensions;
using Boot.Middlewares;

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

app.UseMinimalApi();

app.UseMiddleware<CultureMiddleware>();
app.Run();
