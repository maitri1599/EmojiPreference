using ChatbotAPI.Data;
using ChatbotAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ChatbotAPI.Tests;

public class UserPreferenceRepositoryTests
{
    private AppDbContext GetDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task SavePreference_StoresRecord()
    {
        var db = GetDb();
        var repo = new UserPreferenceRepository(db);

        await repo.SavePreferenceAsync("user1", 2);

        var pref = await db.UserEmojiPreferences.FirstOrDefaultAsync();
        Assert.NotNull(pref);
        Assert.Equal(2, pref.EmojiLevel);
    }

    [Fact]
    public async Task SavePreference_OldRecordBecomesInactive()
    {
        var db = GetDb();
        var repo = new UserPreferenceRepository(db);

        await repo.SavePreferenceAsync("user1", 1);
        await repo.SavePreferenceAsync("user1", 3);

        var active = await db.UserEmojiPreferences
            .Where(p => p.IsActive)
            .ToListAsync();

        Assert.Single(active);
        Assert.Equal(3, active[0].EmojiLevel);
    }

    [Fact]
    public async Task GetActivePreference_ReturnsCorrectLevel()
    {
        var db = GetDb();
        var repo = new UserPreferenceRepository(db);

        await repo.SavePreferenceAsync("user1", 4);

        var pref = await repo.GetActivePreferenceAsync("user1");
        Assert.Equal(4, pref!.EmojiLevel);
    }

    [Fact]
    public async Task RegisterUser_ReturnsId()
    {
        var db = GetDb();
        var repo = new UserPreferenceRepository(db);

        var id = await repo.RegisterUserAsync("user1");

        Assert.True(id > 0);
    }
}
