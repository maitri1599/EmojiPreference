using ChatbotAPI.Helpers;
using Xunit;

namespace ChatbotAPI.Tests;

public class ResponseGeneratorTests
{
    [Fact]
    public void HelloMessage_ReturnsGreeting()
    {
        var result = ResponseGenerator.GetMockResponse("hello", 0);
        Assert.Equal("Hello! How can I help you today.", result);
    }

    [Fact]
    public void ThankYouMessage_ReturnsWelcome()
    {
        var result = ResponseGenerator.GetMockResponse("thank you", 0);
        Assert.Equal("You are welcome.", result);
    }

    [Fact]
    public void UnknownMessage_ReturnsDefault()
    {
        var result = ResponseGenerator.GetMockResponse("random stuff", 0);
        Assert.Equal("Got it. Anything else on your mind.", result);
    }

    [Fact]
    public void EmojiLevel2_AddsEmojisToResponse()
    {
        var result = ResponseGenerator.GetMockResponse("hello", 2);
        Assert.Contains("🙂", result);
    }

    [Fact]
    public void EmojiLevel4_ReturnsUppercase()
    {
        var result = ResponseGenerator.GetMockResponse("hello", 4);
        Assert.Equal(result, result.ToUpper());
    }
}
