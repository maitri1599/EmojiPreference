using ChatbotAPI.Controllers;
using ChatbotAPI.DTOs;
using ChatbotAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ChatbotAPI.Tests;

public class ChatControllerTests
{
    private readonly Mock<IChatService> _mockService;
    private readonly ChatController _controller;

    public ChatControllerTests()
    {
        _mockService = new Mock<IChatService>();
        var logger = new Mock<ILogger<ChatController>>();
        _controller = new ChatController(_mockService.Object, logger.Object);
    }

    [Fact]
    public async Task Send_ValidRequest_ReturnsOk()
    {
        var req = new ChatRequestDto { Message = "hello", EmojiLevel = 1, UserId = "user1" };
        _mockService.Setup(s => s.SendMessageAsync(req))
            .ReturnsAsync(new ChatResponseDto { Reply = "Hello! 🙂" });

        var result = await _controller.Send(req);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Register_EmptyUserId_ReturnsBadRequest()
    {
        var result = await _controller.Register(new RegisterRequestDto { UserId = "" });

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task SavePreference_ValidRequest_ReturnsOk()
    {
        var req = new PreferenceRequestDto { UserId = "user1", EmojiLevel = 2 };
        _mockService.Setup(s => s.SavePreferenceAsync("user1", 2)).Returns(Task.CompletedTask);

        var result = await _controller.SavePreference(req);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SavePreference_InvalidEmojiLevel_ReturnsBadRequest()
    {
        var req = new PreferenceRequestDto { UserId = "user1", EmojiLevel = 10 };

        var result = await _controller.SavePreference(req);

        Assert.IsType<BadRequestObjectResult>(result);
    }
}
