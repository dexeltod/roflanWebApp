using Boot;
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

app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute("default", "{controller=UserRegistration}/{action=Index}/{id?}");
app.UseMiddleware<CultureMiddleware>();
app.Run();
