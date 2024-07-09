using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Exceptions;

public sealed class CannotCreateDraftEpicException : CustomException
{
    public CannotCreateDraftEpicException(JobTitle jobTitle) : base($"Cannot create the Draft Epic for the following job: {jobTitle}.")
    {
    }
}