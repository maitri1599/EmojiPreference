using ChatbotAPI.DTOs;
using ChatbotAPI.Helpers;
using ChatbotAPI.Repositories;
using OpenAI.Chat;

namespace ChatbotAPI.Services;

public class ChatService : IChatService
{
    private readonly IUserPreferenceRepository _prefRepo;
    private readonly ILogger<ChatService> _logger;
    private readonly IConfiguration _config;

    private static readonly string[] _emojiSuffixes =
    [
        "",
        " 🙂",
        " 🙂✨",
        " 😄🔥🎉",
        " 😄🔥🎉🚀✨💯"
    ];

    public ChatService(IUserPreferenceRepository prefRepo, ILogger<ChatService> logger, IConfiguration config)
    {
        _prefRepo = prefRepo;
        _logger = logger;
        _config = config;
    }

    public async Task<ChatResponseDto> SendMessageAsync(ChatRequestDto request)
    {
        var reply = ResponseGenerator.GetMockResponse(request.Message, request.EmojiLevel);
        return new ChatResponseDto { Reply = reply, Timestamp = DateTime.UtcNow };
    }

    public async Task SavePreferenceAsync(string userId, int emojiLevel)
    {
        await _prefRepo.SavePreferenceAsync(userId, emojiLevel);
        _logger.LogInformation("Saved preference for user {UserId}: level {Level}", userId, emojiLevel);
    }

    public async Task<int> RegisterUserAsync(string userId)
    {
        return await _prefRepo.RegisterUserAsync(userId);
    }

    private async Task<string> GetOpenAiReplyAsync(string message, int emojiLevel, string apiKey)
    {
        var client = new ChatClient("gpt-4o-mini", apiKey);

        var systemPrompt = emojiLevel switch
        {
            0 => "You are a helpful assistant. Reply concisely without any emojis.",
            1 => "You are a friendly assistant. Use 1 emoji occasionally in your replies.",
            2 => "You are an upbeat assistant. Use 2 emojis in your replies.",
            3 => "You are an enthusiastic assistant. Use 3-4 emojis and be expressive.",
            4 => "You are an EXTREMELY enthusiastic assistant. Use LOTS of emojis (5+), capitalize key words, be super energetic!!",
            _ => "You are a helpful assistant."
        };

        var chatMessages = new List<OpenAI.Chat.ChatMessage>
        {
            OpenAI.Chat.ChatMessage.CreateSystemMessage(systemPrompt),
            OpenAI.Chat.ChatMessage.CreateUserMessage(message)
        };

        var completion = await client.CompleteChatAsync(chatMessages);
        var rawReply = completion.Value.Content[0].Text;

        if (emojiLevel >= 3 && !rawReply.Contains("😄") && !rawReply.Contains("🔥"))
            rawReply += _emojiSuffixes[emojiLevel];

        return rawReply;
    }
}
