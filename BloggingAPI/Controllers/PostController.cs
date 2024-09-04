using BloggingAPI.Context;
using BloggingAPI.Entities;
using BloggingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BloggingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly BloggingDbContext _dbcontext;

    public PostController(BloggingDbContext context)
    {
        _dbcontext = context ??
            throw new ArgumentNullException(nameof(context));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(
        [FromBody] CreatePostDto dto,
        CancellationToken ct)
    {
        if (dto == null)
        {
            return BadRequest();
        }

        Post post = new(dto.Title, dto.Content, dto.CategoryId, dto.Tags);

        await _dbcontext.Posts.AddAsync(post, ct);
        await _dbcontext.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetPost(
        [FromQuery] GetPostDto request,
        CancellationToken ct)
    {
        var postQuery = _dbcontext.Posts
            .Where(p => string.IsNullOrWhiteSpace(request.Search) ||
            p.Title.ToLower().Contains(request.Search.ToLower()));

        Expression<Func<Post, object>> selectorKey = request.SortItem?.ToLower() switch
        {
            "date" => post => post.CreatedAt,
            "title" => post => post.Title,
            _ => post => post.Id,
        };

        postQuery = request.SortOrder == "desc"
            ? postQuery.OrderByDescending(selectorKey)
            : postQuery.OrderBy(selectorKey);

        var postDtos = await postQuery.Select(p => new PostDto(
            p.Id,
            p.Title,
            p.Content,
            p.CategoryId,
            p.Tags,
            p.CreatedAt,
            p.UpdatedAt)).ToListAsync();

        return Ok(new GetPostResponse(postDtos));
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePost([FromBody] UpdatePostDto dto)
    {
        if (dto == null)
        {
            return BadRequest();
        }

        var postToUpdate = await _dbcontext.Posts
            .FirstOrDefaultAsync(p => p.Id == dto.Id);

        if (postToUpdate == null)
        {
            return BadRequest();
        }

        postToUpdate.Title = dto.Title;
        postToUpdate.Content = dto.Content;
        postToUpdate.CategoryId = dto.CategoryId;
        postToUpdate.Tags = dto.Tags;
        postToUpdate.UpdatedAt = DateTime.UtcNow;

        _dbcontext.Posts.Update(postToUpdate);
        await _dbcontext.SaveChangesAsync();

        return Ok();
    }
}
