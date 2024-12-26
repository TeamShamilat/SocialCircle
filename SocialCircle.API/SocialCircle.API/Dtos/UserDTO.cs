using SocialCircle.API.Models;
using System.ComponentModel.DataAnnotations;

namespace SocialCircle.API.Dtos
{
  
    public class UserDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }

    }


    public class PostDto
    {
        [Key]
        public int PostId { get; set; }
        public string Status { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int LikesCount { get; set; }
        public int CommentCount { get; set; }

        // Foreign Key
        public int UserId { get; set; }
        //public User User { get; set; } // Many-to-One: Posts to User

        //// Navigation properties
        //public ICollection<Comment> Comments { get; set; } // One-to-Many: Post to Comments
        //public ICollection<Like> Likes { get; set; } // One-to-Many: Post to Likes
        //public ICollection<BookMark> BookMarks { get; set; } // One-to-Many: Post to Bookmarks
    }


    public class FriendDTO
    {
        public int FriendId { get; set; }
        public int UserId { get; set; }
        public string FriendUserId { get; set; }
        public DateTime FriendshipDate { get; set; }
        public bool IsAccepted { get; set; }
    }


    public class LikeDTO
    {
        public int LikeId { get; set; }
        public DateTime LikedAt { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }

    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }

    public class BookMarkDTO
    {
        public int BookMarkId { get; set; }
        public DateTime BookmarkedAt { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }


}
