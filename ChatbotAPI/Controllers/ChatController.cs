using ChatbotAPI.DTOs;
using ChatbotAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatbotAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly ILogger<ChatController> _logger;

    public ChatController(IChatService chatService, ILogger<ChatController> logger)
    {
        _chatService = chatService;
        _logger = logger;
    }

    [HttpPost("send")]
    public async Task<IActionResult> Send([FromBody] ChatRequestDto req)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { error = ModelState.Values.SelectMany(v => v.Errors).First().ErrorMessage });

        _logger.LogInformation("Message from {UserId}, level {Level}", req.UserId, req.EmojiLevel);

        var result = await _chatService.SendMessageAsync(req);
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto req)
    {
        if (string.IsNullOrWhiteSpace(req.UserId))
            return BadRequest(new { error = "UserId is required." });

        var userNumber = await _chatService.RegisterUserAsync(req.UserId);
        return Ok(new { userNumber });
    }

    [HttpPost("preference")]
    public async Task<IActionResult> SavePreference([FromBody] PreferenceRequestDto req)
    {
        if (string.IsNullOrWhiteSpace(req.UserId))
            return BadRequest(new { error = "UserId is required." });

        if (req.EmojiLevel < 0 || req.EmojiLevel > 4)
            return BadRequest(new { error = "EmojiLevel must be between 0 and 4." });

        await _chatService.SavePreferenceAsync(req.UserId, req.EmojiLevel);
        return Ok(new { message = "Preference saved." });
    }
}
