using ChatbotAPI.Data;
using ChatbotAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatbotAPI.Repositories;

public class UserPreferenceRepository : IUserPreferenceRepository
{
    private readonly AppDbContext _db;

    public UserPreferenceRepository(AppDbContext db) => _db = db;

    public async Task SavePreferenceAsync(string userId, int emojiLevel)
    {
        var active = await _db.UserEmojiPreferences
            .Where(p => p.UserId == userId && p.IsActive)
            .ToListAsync();

        foreach (var p in active)
            p.IsActive = false;

        _db.UserEmojiPreferences.Add(new UserEmojiPreference
        {
            UserId = userId,
            EmojiLevel = emojiLevel,
            CreatedDate = DateTime.UtcNow,
            IsActive = true
        });

        bool exists = await _db.UserRegistrations.AnyAsync(r => r.UserId == userId);
        if (!exists)
        {
            _db.UserRegistrations.Add(new UserRegistration
            {
                UserId = userId,
                RegisteredAt = DateTime.UtcNow
            });
        }

        await _db.SaveChangesAsync();
    }

    public async Task<UserEmojiPreference?> GetActivePreferenceAsync(string userId)
    {
        return await _db.UserEmojiPreferences
            .Where(p => p.UserId == userId && p.IsActive)
            .OrderByDescending(p => p.CreatedDate)
            .FirstOrDefaultAsync();
    }

    public async Task<int> RegisterUserAsync(string userId)
    {
        var id = await _db.UserRegistrations
            .Where(r => r.UserId == userId)
            .Select(r => r.Id)
            .FirstOrDefaultAsync();

        if (id != 0) return id;

        var reg = new UserRegistration { UserId = userId, RegisteredAt = DateTime.UtcNow };
        _db.UserRegistrations.Add(reg);
        await _db.SaveChangesAsync();
        return reg.Id;
    }
}
