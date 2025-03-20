using System.Globalization;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StoryApp.Domain.Options;

namespace StoryApp.Service;

public class TtsService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;

    public TtsService(IOptions<KeyOptions> options)
    {
        _httpClient = new HttpClient();
        _apiUrl = $"https://texttospeech.googleapis.com/v1/text:synthesize?key={options.Value.TtsApi}";
    }

    public async Task<byte[]> SynthesizeSpeechAsync(string text)
    {

        string langCode = GetLanguage() == "Arabic" ? "ar-XA" : "en-US";
        string voiceName = "en-US-Wavenet-D";
        if (langCode == "ar-XA")
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

        var response = await _httpClient.PostAsync(_apiUrl, content);
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

