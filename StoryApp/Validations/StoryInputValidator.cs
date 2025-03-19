using FluentValidation;
using StoryApp.Domain.Models;

namespace StoryApp.Validations;

public class StoryInputValidator : AbstractValidator<StoryInput>
{
    public StoryInputValidator()
    {
        RuleFor(x => x.Category).IsInEnum();
        RuleFor(x => x.Setting).IsInEnum();
        RuleFor(x => x.Length).IsInEnum();
        RuleFor(x => x.Theme).IsInEnum();
        RuleFor(x => x.Gender).IsInEnum();
        RuleFor(x => x.Job).IsInEnum();
        RuleFor(x => x.Category).IsInEnum();
    }
}
