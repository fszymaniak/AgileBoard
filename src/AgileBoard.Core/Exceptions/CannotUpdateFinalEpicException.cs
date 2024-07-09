using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Exceptions;

public sealed class CannotUpdateFinalEpicException : CustomException
{
    public CannotUpdateFinalEpicException(JobTitle jobTitle) : base($"Cannot update the Final Epic for the following job: {jobTitle}.")
    {
    }
}