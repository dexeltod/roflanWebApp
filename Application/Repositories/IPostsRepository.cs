using Application.DTO;
using Domain.Models;
using FluentValidation.Results;

namespace Application.Repositories;

public interface IPostsRepository
{
    Task AddPost(PostDataTransferObject postData, CancellationToken cancellationToken,
        Action<List<ValidationFailure>> onError);

    Task UpdatePost(long id, string title, string content);
    Task DeletePost(long id);
    Task<Post?> GetPost(long id);
}