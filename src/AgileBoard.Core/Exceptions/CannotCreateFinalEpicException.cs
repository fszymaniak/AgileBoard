using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Exceptions;

public sealed class CannotCreateFinalEpicException : CustomException
{
    public CannotCreateFinalEpicException(JobTitle jobTitle) : base($"Cannot create the Final Epic for the following job: {jobTitle}.")
    {
    }
}