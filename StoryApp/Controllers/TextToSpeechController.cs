using Microsoft.AspNetCore.Mvc;
using StoryApp.Models;
using StoryApp.Service;

namespace StoryApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TextToSpeechController : ControllerBase
{
    private readonly TtsService _ttsService;

    public TextToSpeechController()
    {
        // Replace with your actual API key
        string apiKey = "AIzaSyD3hGvTQ3iYmvEJBrGcZYC6q_E71dLRvxs";
        _ttsService = new TtsService(apiKey);
    }

    [HttpPost("synthesize")]
    public async Task<IActionResult> SynthesizeSpeech([FromBody] GenericModel<string> model)
    {
        if (string.IsNullOrEmpty(model.Data))
        {
            return BadRequest("Text is required.");
        }

        var audioData = await _ttsService.SynthesizeSpeechAsync(model.Data);
        return File(audioData, "audio/mpeg", "output.mp3");
    }
}