using StoryApp.Exceptions.Abstraction;

namespace StoryApp.Exceptions;

public class UnauthorizedException(string message) : Exception, IProblemDetailsProvider
{
    public ServiceProblemDetails GetProblemDetails()
        => new ServiceProblemDetails
        {
            Title = message,
            Type = "Unauthorization",
        };
}
