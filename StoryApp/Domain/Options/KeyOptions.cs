namespace StoryApp.Domain.Options;

public class KeyOptions
{
    public const string Key = "Keys";
    public required string GeminiApi { get; set; }
    public required string TtsApi { get; set; }
}
