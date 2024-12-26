using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialCircle.API.ApplicationContact;
using SocialCircle.API.Dtos;
using SocialCircle.API.Models; // Ensure this namespace is correct
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialCircle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly SocialCircleContext _Contact;

        public PostController(SocialCircleContext contact)
        {
            _Contact = contact;
        }



        [HttpPost("create")]
        public async Task<IActionResult> CreatePost([FromBody] PostDto postDto)
        {
            if (postDto == null)
            {
                return BadRequest("Post data is required.");
            }

            // Map the PostDto to the Post entity (directly or via a service method)
            var post = new Post
            {
                Status = postDto.Status,
                UpdatedAt = DateTime.UtcNow, // Automatically set the current time
                LikesCount = postDto.LikesCount,
                CommentCount = postDto.CommentCount,
                UserId = postDto.UserId
            };

            // Add the new post to the database
            _Contact.Posts.Add(post);
            await _Contact.SaveChangesAsync();

            // Map the saved Post entity to a PostDto to return in the response
            var createdPostDto = new PostDto
            {
                PostId = post.PostId,
                Status = post.Status,
                UpdatedAt = post.UpdatedAt,
                LikesCount = post.LikesCount,
                CommentCount = post.CommentCount,
                UserId = post.UserId
            };

            return CreatedAtAction(nameof(GetPost), new { id = post.PostId }, createdPostDto);
        }






        // GET: api/post
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _Contact.Posts.Include(p => p.User).ToListAsync(); // Include User to get user info
            if (posts == null || !posts.Any())
            {
                return NotFound("No posts found.");
            }

            // Return the posts with necessary data
            var postList = posts.Select(p => new Post
            {
                PostId = p.PostId,
                Status = p.Status,
                UpdatedAt = p.UpdatedAt,
                LikesCount = p.LikesCount,
                CommentCount = p.CommentCount,
                UserId = p.UserId,
                
               
            }).ToList();

            return Ok(postList);
        }

        // GET: api/post/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _Contact.Posts.Include(p => p.User).FirstOrDefaultAsync(p => p.PostId == id);
            if (post == null)
            {
                return NotFound($"Post with ID {id} not found.");
            }

            var postDto = new PostDto
            {
                PostId = post.PostId,
                Status = post.Status,
                UpdatedAt = post.UpdatedAt,
                LikesCount = post.LikesCount,
                CommentCount = post.CommentCount,
                UserId = post.UserId
            };

            return Ok(postDto);
        }

        // POST: api/post
        

        // PUT: api/post/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] PostDto postDto)
        {
            if (postDto == null || id != postDto.PostId)
            {
                return BadRequest("Invalid post data.");
            }

            var post = await _Contact.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound($"Post with ID {id} not found.");
            }

            post.Status = postDto.Status;
            post.LikesCount = postDto.LikesCount;
            post.CommentCount = postDto.CommentCount;
            post.UpdatedAt = DateTime.UtcNow; // Update the timestamp

            _Contact.Posts.Update(post);
            await _Contact.SaveChangesAsync();

            return NoContent(); // Successful update without returning any content
        }

        // DELETE: api/post/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _Contact.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound($"Post with ID {id} not found.");
            }

            _Contact.Posts.Remove(post);
            await _Contact.SaveChangesAsync();

            return NoContent(); // Successful deletion without returning any content
        }
    }
}
