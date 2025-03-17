using StoryApp.Exceptions.Abstraction;

namespace StoryApp.Exceptions;

public class AlreadyExistsException(string message) : Exception, IProblemDetailsProvider
{
    public ServiceProblemDetails GetProblemDetails()
        => new ServiceProblemDetails
        {
            Title = message,
            Type = "Already Exists",
        };
}
