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
public class PostsController : ControllerBase
{
    private readonly BloggingDbContext _dbcontext;

    public PostsController(BloggingDbContext context)
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
        var postsToReturn = await _dbcontext.Posts
            .Where(p => string.IsNullOrWhiteSpace(request.Search) ||
            p.Title.ToLower().Contains(request.Search.ToLower())).ToListAsync();

        return Ok(postsToReturn);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById(Guid id)
    {
        var post = await _dbcontext.Posts.FirstOrDefaultAsync(p => p.Id == id);
        
        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(Guid id)
    {
        var postToDelete = await _dbcontext.Posts.FirstOrDefaultAsync(p => p.Id == id);
        
        if (postToDelete == null)
        {
            return NotFound();
        }

        _dbcontext.Posts.Remove(postToDelete);
        await _dbcontext.SaveChangesAsync();

        return Ok();
    }
}
