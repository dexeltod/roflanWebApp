using Application.Repositories;
using Boot;
using Domain.Models.User;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace xUnitTests;

public class UserTest
{
	[Fact]
	public async Task TestGetCreation()
	{
		WebApplicationFactory<Program> webApplicationFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(
			builder =>
			{
				builder.ConfigureTestServices(
					services =>
					{
						ServiceDescriptor? userRepository =
							services.SingleOrDefault(elem => elem.ServiceType == typeof(IUserRepository));

						if (userRepository != null) services.Remove(userRepository);

						var mockService = new Mock<IUserRepository>();

						mockService
							.Setup(repo => repo.GetUserById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
							.ReturnsAsync(new User { Id = 1, Name = "John Doe" });

						services.AddTransient(_ => mockService.Object);
					}
				);
			}
		);

		// Получаем сервис IUserRepository через фабрику
		var repository = webApplicationFactory.Services.GetRequiredService<IUserRepository>();

		// Выполняем тест
		User? user = await repository.GetUserById(1, CancellationToken.None);
		Assert.NotNull(user);
		Assert.Equal("John Doe", user.Name);
	}
}
