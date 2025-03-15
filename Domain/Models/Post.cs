namespace Domain.Models;

public class Post
{
	public Post()
	{
	}

	public Post(string title, string content, User.User author)
	{
		Title = title;
		Content = content;
		Author = author;
	}

	public long Id { get; init; }
	public long AuthorId { get; set; }
	public string Title { get; set; }
	public string Content { get; set; }
	public DateTime CreatedAt { get; init; }
	public User.User Author { get; set; }
}