namespace BloggingAPI.Models;

public record UpdatePostDto(Guid Id,
    string Title, 
    string Content, 
    int CategoryId, 
    string[] Tags);
