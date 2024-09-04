namespace BloggingAPI.Models;

public record PostDto(Guid Id, 
    string Title, 
    string Content, 
    int CategoryId,
    string[] Tags,
    DateTime CreatedAt,
    DateTime UpdatedAt);
