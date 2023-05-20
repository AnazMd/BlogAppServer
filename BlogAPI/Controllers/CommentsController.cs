using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Data;
using BlogModels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/posts/{postId}/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly BlogDbContext _context;

        public CommentsController(BlogDbContext context)
        {
            _context = context;
        }

        // Get all comments for a specific post
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments(int postId)
        {
            return await _context.Comments.Where(c => c.PostId == postId).ToListAsync();
        }

        // Get a specific comment by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int postId, int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id && c.PostId == postId);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // Create a new comment
        [HttpPost]
        public async Task<ActionResult<Comment>> CreateComment(int postId, Comment comment)
        {
            Console.WriteLine($"post id: {postId}, comment object: {System.Text.Json.JsonSerializer.Serialize(comment)}");
            comment.PostId = postId;
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { postId, id = comment.Id }, comment);
        }

        // Update an existing comment
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int postId, int id, Comment comment)
        {
            if (id != comment.Id || postId != comment.PostId)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Delete a comment
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int postId, int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id && c.PostId == postId);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}

