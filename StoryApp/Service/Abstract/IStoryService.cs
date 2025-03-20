using StoryApp.Domain.Models;

namespace StoryApp.Service.Abstract;

public interface IStoryService
{
    public Task<string> GenerateStoryAsync(StoryInput story);
    public Task<string> SummaryStory(string story);
    public Task<string> GetLessons(string story);
}
