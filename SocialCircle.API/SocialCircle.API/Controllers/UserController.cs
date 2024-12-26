using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialCircle.API.Dtos;
using SocialCircle.API.Models; // Ensure this namespace is correct
using SocialCircle.API.ApplicationContact; // Make sure this is the correct namespace for your DbContext
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialCircle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SocialCircleContext _Contact;

        public UserController(SocialCircleContext contact)
        {
            _Contact = contact;
        }

        // GET: api/user
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _Contact.Users.ToListAsync();
            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }

            // Return the users without sensitive data like HashPassword
            var userList = users.Select(u => new User
            {
                UserId = u.UserId,
                UserName = u.UserName,
                Email = u.Email,
                Posts = null, // Optional: If you don't want to return the posts, set to null
                Likes = null, // Optional: If you don't want to return likes, set to null
                Comments = null, // Optional: If you don't want to return comments, set to null
                BookMarks = null, // Optional: If you don't want to return bookmarks, set to null
                Friends = null, // Optional: If you don't want to return friends, set to null
                FriendOf = null // Optional: If you don't want to return friend relationships, set to null
            }).ToList();

            return Ok(userList);
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _Contact.Users.FindAsync(id); // Use FindAsync for direct lookup by primary key
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            var userDto = new UserDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email
            };

            return Ok(userDto);
        }

        // POST: api/user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is required.");
            }

            var user = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                HashPassword = "hashedpassword" // Assume password hashing logic is handled
            };

            // Add user to the context and save changes
            _Contact.Users.Add(user);
            await _Contact.SaveChangesAsync();

            var createdUserDto = new UserDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                HashPassword = user.HashPassword
            };

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, createdUserDto);
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO userDto)
        {
            if (userDto == null || id != userDto.UserId)
            {
                return BadRequest("Invalid user data.");
            }

            var user = await _Contact.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            // You can update password logic if needed

            _Contact.Users.Update(user);
            await _Contact.SaveChangesAsync();

            return NoContent(); // Successful update without returning any content
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _Contact.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            _Contact.Users.Remove(user);
            await _Contact.SaveChangesAsync();

            return NoContent(); // Successful deletion without returning any content
        }
    }
}
