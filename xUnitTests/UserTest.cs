using Application.Repositories;
using Domain.Models;
using Moq;

namespace xUnitTests;

public class UserTest
{
	[Fact]
	public async Task TestGetCreation()
	{
		var repository = new Mock<IUserRepository>();

		repository.Setup(userRepository => userRepository.GetUserById(1, It.IsAny<CancellationToken>()));

		User? user = await repository.Object.GetUserById(1, new CancellationToken());

		Assert.NotNull(user);
	}
}
