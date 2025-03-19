namespace StoryApp.Service.Abstract;

public interface IGeminiService
{
    Task<string> MessageGeminiAsync(string message);
}
