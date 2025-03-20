using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using StoryApp.Domain.Models;
using StoryApp.Domain.Options;
using StoryApp.Service.Abstract;

namespace StoryApp.Service;

public class GeminiService : IGeminiService
{
    private readonly string _apiUrl;
    private readonly HttpClient _httpClient;

    public GeminiService(IOptions<KeyOptions> options)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://generativelanguage.googleapis.com");
        _apiUrl = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={options.Value.GeminiApi}";
    }

    public async Task<string> MessageGeminiAsync(string message)
    {
        var requestBody = new
        {
            contents = new[]
            {
                    new
                    {
                        parts = new[]
                        {
                            new { text = message }
                        }
                    }
                }
        };
        var jsonPayload = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_apiUrl, content);
        response.EnsureSuccessStatusCode();
        GeminiResponse? geminiResponse = await response.Content.ReadFromJsonAsync<GeminiResponse>();
        if (geminiResponse is null)
            throw new Exception("");
        return GetFinalMessage(geminiResponse) ?? throw new Exception("");
    }

    private string? GetFinalMessage(GeminiResponse geminiResponse)
        => geminiResponse.Candidates.Select(x => x.Content).SelectMany(x => x.Parts).Select(x => x.Text).FirstOrDefault();

}
