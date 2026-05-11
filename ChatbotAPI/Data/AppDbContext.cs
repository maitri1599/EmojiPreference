using ChatbotAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatbotAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<UserEmojiPreference> UserEmojiPreferences { get; set; }
    public DbSet<UserRegistration> UserRegistrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEmojiPreference>(entity =>
        {
            entity.HasIndex(e => new { e.UserId, e.IsActive });
        });

        modelBuilder.Entity<UserRegistration>(entity =>
        {
            entity.HasIndex(e => e.UserId).IsUnique();
        });
    }
}
