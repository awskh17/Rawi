namespace StoryApp.Models;

public record GenericModel<T>(T Data);
public record GenericModelSummary<T,S>(T Data,S Lessons);