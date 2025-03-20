using StoryApp.Domain.Enums;

namespace StoryApp.Domain.Models;

public record StoryInput
(
    StoryCategory Category,
    StorySettingEnum Setting,
    StoryLengthEnum Length,
    StoryThemeEnum Theme,
    GenderEnum Gender,
    JobEnum Job,
    int NumberOfCharecters,
    string NameOfMainCharcter,
    bool Age
);