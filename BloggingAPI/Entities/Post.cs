namespace BloggingAPI.Entities;

public class Post
{
    public Post(string title, string content, int categoryId, string[] tags)
    {
        Title = title;
        Content = content;
        CategoryId = categoryId;
        Tags = tags;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string[] Tags { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}
