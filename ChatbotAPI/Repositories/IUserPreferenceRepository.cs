using ChatbotAPI.Models;

namespace ChatbotAPI.Repositories;

public interface IUserPreferenceRepository
{
    Task SavePreferenceAsync(string userId, int emojiLevel);
    Task<UserEmojiPreference?> GetActivePreferenceAsync(string userId);
    Task<int> RegisterUserAsync(string userId);
}
