using System.ComponentModel.DataAnnotations;

namespace ChatbotAPI.DTOs;

public class ChatRequestDto
{
    [Required(ErrorMessage = "Message is required.")]
    [MinLength(1, ErrorMessage = "Message cannot be empty.")]
    [MaxLength(500, ErrorMessage = "Message cannot exceed 500 characters.")]
    public string Message { get; set; } = string.Empty;

    [Range(0, 4, ErrorMessage = "EmojiLevel must be between 0 and 4.")]
    public int EmojiLevel { get; set; }

    [Required(ErrorMessage = "UserId is required.")]
    [MaxLength(100)]
    public string UserId { get; set; } = string.Empty;
}
