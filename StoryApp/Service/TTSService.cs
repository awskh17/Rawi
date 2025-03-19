using System.Globalization;
using System.Text;
using Newtonsoft.Json;

namespace StoryApp.Service;

public class TtsService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public TtsService(string apiKey)
    {
        _httpClient = new HttpClient();
        _apiKey = apiKey;
    }

    public async Task<byte[]> SynthesizeSpeechAsync(string text)
    {
        var url = $"https://texttospeech.googleapis.com/v1/text:synthesize?key={_apiKey}";

        string langCode = GetLanguage() == "ar" ? "ar-XA" : "en-US";
        string voiceName = "en-US-Wavenet-D";
        if (langCode == "ar")
        {
            voiceName = "ar-XA-Wavenet-B";
        }

        var requestBody = new
        {
            input = new { text = text },
            voice = new
            {
                languageCode = langCode,
                name = voiceName,
                ssmlGender = "MALE"
            },
            audioConfig = new
            {
                audioEncoding = "MP3",
                pitch = -5.0
            }
        };

        var jsonRequest = JsonConvert.SerializeObject(requestBody);
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error calling Google TTS API: " + response.ReasonPhrase);
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        dynamic result = JsonConvert.DeserializeObject(jsonResponse);

        return Convert.FromBase64String(result.audioContent.ToString());
    }
    private string GetLanguage()
            => CultureInfo.CurrentCulture.Name switch
            {
                "ar" => "Arabic",
                _ => "English"
            };
}

