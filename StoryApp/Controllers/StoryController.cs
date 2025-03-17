using System.Globalization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StoryApp.Models;
using StoryApp.Service.Abstract;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;


namespace StoryApp.Controllers;

[ApiController]
[Route("[Controller]")]
[EnableCors("AllowAllOrigins")]
public class StoryController : ControllerBase
{
    private readonly IStoryService _storyService;

    public StoryController(IStoryService storyService)
    {
        _storyService = storyService;
    }

    [HttpGet("generate-story")]
    public async Task<GenericModel<string>> StoryAsync([FromQuery] StoryInput input)
    {
        string story = await _storyService.GenerateStoryAsync(input);
        Console.WriteLine(CultureInfo.CurrentCulture.Name);
        return new GenericModel<string>(story);
    }

    [HttpPost("summary-story")]
    public async Task<GenericModel<string>> SummaryString([FromBody] GenericModel<string> model)
    {
        string story = await _storyService.SummaryStory(model.Data);
        return new GenericModel<string>(story);
    }
}