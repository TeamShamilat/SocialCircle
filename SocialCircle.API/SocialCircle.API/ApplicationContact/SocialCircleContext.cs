using Microsoft.EntityFrameworkCore;
using SocialCircle.API.Models;

namespace SocialCircle.API.ApplicationContact
{
    public class SocialCircleContext : DbContext
    {
        public SocialCircleContext(DbContextOptions<SocialCircleContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<BookMark> BookMarks { get; set; }

        // Configuring the relationships in OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User to Post
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User to Comment
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete for User

            // Configure Comment -> Post relationship
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade); // Allow cascading delete for Post

            // Friend to User (Self-referencing)
            modelBuilder.Entity<Friend>()
                .HasOne(f => f.User)
                .WithMany(u => u.Friends)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.FriendUser)
                .WithMany(u => u.FriendOf)
                .HasForeignKey(f => f.FriendUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User to Like
            modelBuilder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // User to BookMark
            modelBuilder.Entity<BookMark>()
                .HasOne(b => b.User)  // A BookMark belongs to one User
                .WithMany(u => u.BookMarks) // A User can have many BookMarks
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete for User

            // BookMark to Post
            modelBuilder.Entity<BookMark>()
                .HasOne(b => b.Post)  // A BookMark belongs to one Post
                .WithMany(p => p.BookMarks) // A Post can have many BookMarks
                .HasForeignKey(b => b.PostId)
                .OnDelete(DeleteBehavior.Cascade); // Allow cascading delete for Post
        }
    }
}
