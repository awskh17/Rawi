using FluentValidation;
using StoryApp.Models;

namespace StoryApp.Validations;

public class GenericModelStringValidator : AbstractValidator<GenericModel<string>>
{
    public GenericModelStringValidator()
    {
        RuleFor(x => x.Data).NotEmpty();
    }
}
