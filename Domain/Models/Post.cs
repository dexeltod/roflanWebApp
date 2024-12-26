namespace Domain.Models;

public class Post
{
    // Конструктор без параметров
    public Post()
    {
    }

    public Post(string title, string content, User author)
    {
        Title = title;
        Content = content;
        Author = author;
        AuthorId = author.Id;
    }

    // Остальные свойства
    public long Id { get; init; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; init; }
    public long AuthorId { get; init; }
    public User Author { get; init; }
}