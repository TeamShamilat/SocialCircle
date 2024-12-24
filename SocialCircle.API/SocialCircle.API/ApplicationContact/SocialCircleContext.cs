using Microsoft.EntityFrameworkCore;
using SocialCircle.API.Models;

namespace SocialCircle.API.ApplicationContact
{
    public class SocialCircleContext : DbContext
    {
        public SocialCircleContext(DbContextOptions<SocialCircleContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<BookMark> BookMarks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite key for Friend entity
            modelBuilder.Entity<Friend>()
                .HasKey(f => new { f.UserId, f.FriendId });

            // Self-referencing many-to-many relationship for Friends
            modelBuilder.Entity<Friend>()
                .HasOne(f => f.User)
                .WithMany(u => u.Friends)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.FriendUser)
                .WithMany()
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.Restrict);

            // User-Post: One-to-Many
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User-Like: One-to-Many
            modelBuilder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Post-Like: One-to-Many
            modelBuilder.Entity<Like>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // User-BookMark: One-to-Many
            modelBuilder.Entity<BookMark>()
                .HasOne(b => b.User)
                .WithMany(u => u.BookMarks)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Post-BookMark: One-to-Many
            modelBuilder.Entity<BookMark>()
                .HasOne(b => b.Post)
                .WithMany(p => p.BookMarks)
                .HasForeignKey(b => b.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            // Additional configurations can be added here
        }
    }
}
    