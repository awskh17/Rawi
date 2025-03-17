using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using StoryApp.Exceptions;

namespace StoryApp.ActionFilters;

public class ValidatorActionFilter(IServiceProvider serviceProvider) : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var argument in context.ActionArguments)
        {
            if (argument.Value != null)
            {
                var inputType = argument.Value.GetType();

                var validatorType = typeof(IValidator<>).MakeGenericType(inputType);

                var validator = serviceProvider.GetService(validatorType);

                if (validator != null)
                {
                    Console.WriteLine($"Validator found for input type: {inputType.Name}");

                    var validationContextType = typeof(ValidationContext<>).MakeGenericType(inputType);
                    var validationContext = Activator.CreateInstance(validationContextType, argument.Value);
                    if (validationContext is null)
                        continue;
                    var validationResult = ((IValidator)validator).Validate((IValidationContext)validationContext);

                    if (!validationResult.IsValid)
                    {
                        throw new InvalidArguementException(
                            validationResult.Errors.Select(x => (x.PropertyName, x.ErrorMessage)).ToList());
                    }
                }
                else
                {
                    Console.WriteLine($"No validator found for input type: {inputType.Name}");
                }
            }
        }
    }
}
