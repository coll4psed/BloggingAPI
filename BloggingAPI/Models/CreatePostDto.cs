namespace BloggingAPI.Models;

public record CreatePostDto(string Title, 
    string Content,
    int CategoryId,
    string[] Tags);
