using System.ComponentModel.DataAnnotations;

namespace SocialCircle.API.Models;
public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string HashPassword { get; set; }

    // Navigation properties
    public ICollection<Post> Posts { get; set; } // One-to-Many: User to Posts
    public ICollection<Like> Likes { get; set; } // One-to-Many: User to Likes
    public ICollection<Comment> Comments { get; set; } // One-to-Many: User to Comments
    public ICollection<BookMark> BookMarks { get; set; } // One-to-Many: User to Bookmarks
    public ICollection<Friend> Friends { get; set; } // Self-referencing Many-to-Many
    public ICollection<Friend> FriendOf { get; set; } // Self-referencing Many-to-Many
}



public class Post
{
    [Key]
    public int PostId { get; set; }
    public string Status { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public int LikesCount { get; set; }
    public int CommentCount { get; set; }

    // Foreign Key
    public int UserId { get; set; }
    public User User { get; set; } // Many-to-One: Posts to User

    // Navigation properties
    public ICollection<Comment> Comments { get; set; } // One-to-Many: Post to Comments
    public ICollection<Like> Likes { get; set; } // One-to-Many: Post to Likes
    public ICollection<BookMark> BookMarks { get; set; } // One-to-Many: Post to Bookmarks
}


public class Friend
{
    [Key]
    public int FriendId { get; set; }

    public int UserId { get; set; }   // The user initiating the friendship
    public int FriendUserId { get; set; } // The friend being added

    public DateTime FriendshipDate { get; set; } = DateTime.UtcNow;
    public bool IsAccepted { get; set; } = false;

    // Navigation properties
    public User User { get; set; }         // The user initiating the friendship
    public User FriendUser { get; set; }  // The friend being added
}


public class Like
{
    [Key]
    public int LikeId { get; set; }
    public DateTime LikedAt { get; set; } = DateTime.UtcNow;

    // Foreign Keys
    public int PostId { get; set; }
    public Post Post { get; set; } // Many-to-One: Likes to Post

    public int UserId { get; set; }
    public User User { get; set; } // Many-to-One: Likes to User
}

public class Comment
{
    [Key]
    public int CommentId { get; set; }

    public string Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Foreign Keys
    public int PostId { get; set; }
    public Post Post { get; set; } // Many-to-One: Comments to Post

    public int UserId { get; set; }
    public User User { get; set; } // Many-to-One: Comments to User
}


public class BookMark
    {
        [Key]
        public int BookMarkId { get; set; }
        public DateTime BookmarkedAt { get; set; } = DateTime.UtcNow;

        // Foreign key to Post
        public int PostId { get; set; }
        public Post Post { get; set; }

        // Foreign key to User
        public int UserId { get; set; }
        public User User { get; set; }
    }