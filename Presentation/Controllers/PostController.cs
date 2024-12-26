using Application.DTO;
using FluentValidation.Results;
using Infrastructure.Repositories;
using Infrastructure.Validation;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class PostController : ControllerBase
{
    private readonly PostsRepository _postsRepository;

    public PostController(PostsRepository postsRepository)
    {
        _postsRepository = postsRepository ?? throw new ArgumentNullException(nameof(postsRepository));
    }

    [HttpPost("add-post")]
    public async Task<IActionResult> AddPost([FromForm] PostDataTransferObject postData,
        CancellationToken cancellationToken)
    {
        await _postsRepository.AddPost(postData, cancellationToken, OnError);

        return Ok("Post added");
    }

    private async void OnError(List<ValidationFailure> obj)
    {
        
    }

    [HttpPut("update-post")]
    public async Task<IActionResult> UpdatePost()
    {
        var form = GetForm();

        await _postsRepository.UpdatePost(
            int.Parse(form["postId"].ToString()),
            form["title"].ToString(),
            form["content"].ToString()
        );

        return Ok("Post updated");
    }

    [HttpDelete("delete-post")]
    public async Task<IActionResult> DeletePost()
    {
        var form = GetForm();
        await _postsRepository.DeletePost(
            int.Parse(
                form["postId"].ToString()
            )
        );

        return Ok("Post deleted");
    }

    [HttpGet("get-post")]
    public async Task<IActionResult> GetPost()
    {
        var form = GetForm();
        var post = await _postsRepository.GetPost(int.Parse(form["postId"].ToString()));

        return Ok(post);
    }

    private IFormCollection GetForm()
    {
        return HttpContext.Request.Form;
    }
}