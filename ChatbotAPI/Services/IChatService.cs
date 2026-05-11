using ChatbotAPI.DTOs;

namespace ChatbotAPI.Services;

public interface IChatService
{
    Task<ChatResponseDto> SendMessageAsync(ChatRequestDto request);
    Task SavePreferenceAsync(string userId, int emojiLevel);
    Task<int> RegisterUserAsync(string userId);
}
