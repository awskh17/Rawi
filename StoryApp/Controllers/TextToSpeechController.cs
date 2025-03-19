using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StoryApp.Domain.Models;
using StoryApp.Service;

namespace StoryApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[EnableCors("AllowAllOrigins")]
public class TextToSpeechController(TtsService ttsService) : ControllerBase
{

    [HttpPost("synthesize")]
    public async Task<IActionResult> SynthesizeSpeech([FromBody] GenericModel<string> model)
    {
        if (string.IsNullOrEmpty(model.Data))
        {
            return BadRequest("Text is required.");
        }

        var audioData = await ttsService.SynthesizeSpeechAsync(model.Data);
        return File(audioData, "audio/mpeg", "output.mp3");
    }
}