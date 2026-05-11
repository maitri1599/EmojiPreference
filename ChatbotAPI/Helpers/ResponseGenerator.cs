namespace ChatbotAPI.Helpers;

public static class ResponseGenerator
{
    public static string GetMockResponse(string message, int emojiLevel)
    {
        var lower = message.ToLowerInvariant();
        string reply;

        if (lower.Contains("hello") || lower.Contains("hi") || lower.Contains("hey"))
        {
            reply = "Hello! How can I help you today.";
        }
        else if (lower.Contains("how are you") || lower.Contains("how r u"))
        {
            reply = "I am doing well, thanks for asking.";
        }
        else if (lower.Contains("thank") || lower.Contains("thx"))
        {
            reply = "You are welcome.";
        }
        else if (lower.Contains("bye") || lower.Contains("goodbye"))
        {
            reply = "Goodbye! Have a great day.";
        }
        else
        {
            reply = "Got it. Anything else on your mind.";
        }

        if (emojiLevel == 0)
            return reply;
        else if (emojiLevel == 1)
            return reply + " 🙂";
        else if (emojiLevel == 2)
            return reply + " 🙂✨";
        else if (emojiLevel == 3)
            return reply + " 😄🔥🎉";
        else
            return reply.ToUpper() + " 😄🔥🎉🚀✨💯";
    }
}
