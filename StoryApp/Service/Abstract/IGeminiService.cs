namespace StoryApp.Service.Abstract;

internal interface IGeminiService
{
    Task<string> MessageGeminiAsync(string message);
}
