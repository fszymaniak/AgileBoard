using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Exceptions;

public sealed class CannotUpdateDraftEpicException : CustomException
{
    public CannotUpdateDraftEpicException(JobTitle jobTitle) : base($"Cannot update the Draft Epic for the following job: {jobTitle}.")
    {
    }
}