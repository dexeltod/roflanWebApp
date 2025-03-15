using Application.DTO;
using Application.Repositories;
using Domain.Models;
using FluentValidation.Results;
using Infrastructure.Contexts;
using Infrastructure.Factories;
using Infrastructure.Validation;

namespace Infrastructure.Repositories;

public class PostsRepository : IPostsRepository
{
	private readonly ApplicationContext _context;
	private readonly PostFactory _postFactory;
	private readonly PostValidator _postValidator;

	public PostsRepository(ApplicationContext context, PostFactory postFactory, PostValidator postValidator)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
		_postFactory = postFactory ?? throw new ArgumentNullException(nameof(postFactory));
		_postValidator = postValidator ?? throw new ArgumentNullException(nameof(postValidator));
	}

	public async Task AddPost(
		PostDataTransferObject postData,
		CancellationToken cancellationToken,
		Action<List<ValidationFailure>> onError)
	{
		ValidationResult? validation = await _postValidator.ValidateAsync(postData, cancellationToken);

		if (validation.IsValid == false)
		{
			onError.Invoke(validation.Errors);
			return;
		}

		Post post = await _postFactory.Create(postData);

		await _context.Posts.AddAsync(post, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task UpdatePost(long id, string title, string content)
	{
		Post? post = await GetPost(id);

		if (post == null) throw new ArgumentNullException(nameof(post));

		post.Title = title;
		post.Content = content;

		_context.Posts.Update(post);
		await _context.SaveChangesAsync();
	}

	public async Task DeletePost(long id)
	{
		Post? post = await GetPost(id);
		if (post == null) throw new ArgumentNullException(nameof(post));

		_context.Posts.Remove(post);
		await _context.SaveChangesAsync();
	}

	public async Task<Post?> GetPost(long id)
	{
		Post? post = await _context.Posts.FindAsync(id);
		if (post == null) throw new ArgumentNullException(nameof(post));

		return post;
	}
}