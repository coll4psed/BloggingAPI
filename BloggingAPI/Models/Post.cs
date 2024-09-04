﻿namespace BloggingAPI.Models;

public class Post
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Category { get; set; }
    public string[] Tags { get; set; }
}
