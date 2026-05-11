# EmojiPreference

A simple chatbot where users can control how many emojis appear in the responses (0 to extreme).

## Project Structure:
    EmojiChatbot/
    ChatbotAPI – .NET 8 backend API
    ChatbotAPI.Tests – backend unit tests
    chatbot-frontend – React frontend
    DOCUMENTATION.md – detailed project explanation

## Tech Stack

### Frontend:
    React
    Axios
    LocalStorage
    React hooks

### Backend:
    ASP.NET Core Web API (.NET 8)
    Entity Framework Core
    SQL Server Express
    xUnit + Moq for testing

## Requirements
    .NET 8 SDK
    Node.js (v18+)
    SQL Server Express

How to run the project

### Backend:
  1. Open ChatbotAPI/appsettings.json and update SQL Server connection string if needed
  2. Run backend:
     cd ChatbotAPI
     dotnet run
API runs at: http://localhost:5000

### Frontend:
  1. Go to chatbot-frontend folder
  2. Run:
     npm install
     npm run dev
Open: http://localhost:3000

## Running tests
    Backend tests: dotnet test
    Frontend tests: npm test

## Key Features
    Chatbot UI with emoji control (0–extreme levels)
    Mock response engine (no real AI used)
    Persistent user preference using localStorage
    Each browser gets a unique User ID
    User ID mapped to sequential User N in database
    Chat history maintained per session
    Clean separation between frontend and backend

Note: Real OpenAI integration is already prepared in backend code but commented out.
To enable: Add API key in appsettings.json under OpenAI:ApiKey and Uncomment OpenAI call in ChatService.cs