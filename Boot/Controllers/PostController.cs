using Application.DTO;
using Application.Repositories;
using Domain.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boot.Controllers;

public class PostController : ControllerBase
{
	private readonly IPostsRepository _postsRepository;

	public PostController(IPostsRepository postsRepository) =>
		_postsRepository =
			postsRepository ?? throw new ArgumentNullException(nameof(postsRepository));

	[Authorize]
	[HttpPost("add-post")]
	public async Task<IActionResult> AddPost([FromBody] PostDataTransferObject postData, CancellationToken cancellationToken)
	{
		await _postsRepository.AddPost(postData, cancellationToken, OnError);

		return Ok("Post added");
	}

	[HttpDelete("delete-post")]
	public async Task<IActionResult> DeletePost()
	{
		IFormCollection form = GetForm();
		await _postsRepository.DeletePost(
			int.Parse(form["postId"].ToString())
		);

		return Ok("Post deleted");
	}

	[HttpGet("get-post")]
	public async Task<IActionResult> GetPost()
	{
		IFormCollection form = GetForm();
		Post? post = await _postsRepository.GetPost(int.Parse(form["postId"].ToString()));

		return Ok(post);
	}

	[HttpPut("update-post")]
	public async Task<IActionResult> UpdatePost()
	{
		IFormCollection form = GetForm();

		await _postsRepository.UpdatePost(
			int.Parse(form["postId"].ToString()),
			form["title"].ToString(),
			form["content"].ToString()
		);
		return Ok("Post updated");
	}

	private IFormCollection GetForm() => HttpContext.Request.Form;

	private void OnError(List<ValidationFailure> obj)
	{
		throw new NotImplementedException();
	}
}