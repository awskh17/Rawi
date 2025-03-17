namespace StoryApp.Service;

public class VoiceRssTtsService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "ec8356a955c640838358acd5e3fd6ce4";

    public VoiceRssTtsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> ConvertTextToSpeech(string text)
    {
        string url = $"https://api.voicerss.org/?key={_apiKey}&hl=en-us&src={Uri.EscapeDataString(text)}&c=MP3";
        string filePath = $"audio/{Guid.NewGuid()}.mp3";

        byte[] audioBytes = await _httpClient.GetByteArrayAsync(url);
        await File.WriteAllBytesAsync(filePath, audioBytes);

        return filePath;
    }
}
