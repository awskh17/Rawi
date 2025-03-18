using System.Globalization;
using StoryApp.Models;
using StoryApp.Service.Abstract;

namespace StoryApp.Service;

public class StoryService : IStoryService
{
    IGeminiService geminiService = new GeminiService();
    public async Task<string> GenerateStoryAsync(StoryInput story)
    {
        var a = GetLanguage();
        return await geminiService.MessageGeminiAsync(
$"Generate a creative {story.Category} story with {story.NumberOfCharecters} characters.The main character is {story.NameOfMainCharcter}, a {story.Gender} {story.Job}. The story is set in {story.Setting}.The theme of the story is {story.Theme}. The story should be {story.Length} long, with creative ending, generate all text in {GetLanguage()} language and humanize it and just give me the story without any word such as certainly and remove any unnecessary symbol such as #.");

    }

    public async Task<string> GetLessons(string story)
    {
        return await geminiService.MessageGeminiAsync($"give me three lessons from this story {story} . generate all text in {GetLanguage()} language and humanize it and put them like 'lesson1&&lesson2&&lesson3'");
    }

    public async Task<string> SummaryStory(string story)
    {
        return await geminiService.MessageGeminiAsync($"summary this story in engaging way and generate all text in {GetLanguage()} language and humanize it:{story}");
    }

    private string GetLanguage()
        => CultureInfo.CurrentCulture.Name switch
        {
            "ar" => "Arabic",
            _ => "English"
        };
}
