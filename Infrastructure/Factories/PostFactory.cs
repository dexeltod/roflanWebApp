using Application.DTO;
using Domain.Models;
using Domain.Models.User;
using Infrastructure.Contexts;

namespace Infrastructure.Factories;

public class PostFactory
{
	private readonly ApplicationContext _applicationContext;

	public PostFactory(ApplicationContext applicationContext) =>
		_applicationContext =
			applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));

	public async Task<Post> Create(PostDataTransferObject postData)
	{
		User? userFromBd = await _applicationContext.Users.FindAsync(postData.AuthorId);
		if (userFromBd == null) throw new ArgumentNullException(nameof(userFromBd));

		return new Post(postData.Title, postData.Content, userFromBd);
	}
}