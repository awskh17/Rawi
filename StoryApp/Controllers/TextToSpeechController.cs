using Microsoft.AspNetCore.Mvc;
using StoryApp.Service;

namespace StoryApp.Controllers;

[ApiController]
[Route("[Controller]")]
public class TextToSpeechController : ControllerBase
{
    private readonly VoiceRssTtsService _voiceRssTtsService;

    public TextToSpeechController(VoiceRssTtsService voiceRssTtsService)
    {
        _voiceRssTtsService = voiceRssTtsService;
    }

    [HttpPost]
    public async Task<String> ConvertTextToSpeech(string text)
    {
        //if (string.IsNullOrEmpty(text))
        //{
        //    return BadRequest("Text cannot be empty");
        //}

        string audioFilePath = await _voiceRssTtsService.ConvertTextToSpeech(text);

        // إرجاع المسار الصوتي للعرض في الصفحة
        return audioFilePath;
    }
}
