using StoryApp.Exceptions.Abstraction;

namespace StoryApp.Exceptions;

public class NotFoundException(string message) : Exception, IProblemDetailsProvider
{
    public ServiceProblemDetails GetProblemDetails()
        => new ServiceProblemDetails
        {
            Title = message,
            Type = "Failed Precondition",
        };
}
