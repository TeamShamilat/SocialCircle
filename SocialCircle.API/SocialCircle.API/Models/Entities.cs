namespace SocialCircle.API.Models;
public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public int Phone { get; set; }
    public string HashPassword { get; set; }

    // Navigation Properties
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Like> Likes { get; set; } = new List<Like>();
    public ICollection<BookMark> BookMarks { get; set; } = new List<BookMark>();
    public ICollection<Friend> Friends { get; set; } = new List<Friend>();
}


public class Post
{
    public int PostId { get; set; }
    public int UserId { get; set; } // Foreign Key to User
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
    public bool IsPublic { get; set; }

    // Navigation Properties
    public User User { get; set; } 
    public ICollection<Like> Likes { get; set; } // Navigation property for Likes
    public ICollection<BookMark> BookMarks { get; set; } // Navigation property for BookMarks
}

public class Friend
{
    public int UserId { get; set; } 
    public int FriendId { get; set; } 
    public DateTime FriendshipDate { get; set; }
    public bool IsAccepted { get; set; } 

    // Navigation Properties
    public User User { get; set; } // Reference to the user
    public User FriendUser { get; set; } // Reference to the friend
}


public class Like
{
    public int LikeId { get; set; }
    public int UserId { get; set; } // Foreign Key
    public int PostId { get; set; } // Foreign Key
    public DateTime LikedAt { get; set; }

    // Navigation Properties
    public User User { get; set; } 
    public Post Post { get; set; } 
}

public class BookMark
{
    public int BookMarkId { get; set; }
    public int UserId { get; set; } // Foreign Key
    public int PostId { get; set; } // Foreign Key
    public DateTime BookmarkedAt { get; set; }

    // Navigation Properties
    public User User { get; set; } // Navigation property to User
    public Post Post { get; set; } // Navigation property to Post
}
    