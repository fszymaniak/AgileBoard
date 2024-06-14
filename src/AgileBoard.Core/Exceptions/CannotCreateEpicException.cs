using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Exceptions;

public sealed class CannotCreateEpicException : CustomException
{
    public CannotCreateEpicException(JobTitle jobTitle) : base("Cannot create Epic for the following job: {jobTitle}.")
    {
    }
}