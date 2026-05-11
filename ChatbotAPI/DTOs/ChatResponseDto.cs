namespace ChatbotAPI.DTOs;

public class ChatResponseDto
{
    public string Reply { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

public class ErrorResponseDto
{
    public string Error { get; set; } = string.Empty;

    public int StatusCode { get; set; }
}

public class PreferenceRequestDto
{
    public string UserId { get; set; } = string.Empty;

    public int EmojiLevel { get; set; }
}

public class RegisterRequestDto
{
    public string UserId { get; set; } = string.Empty;
}
