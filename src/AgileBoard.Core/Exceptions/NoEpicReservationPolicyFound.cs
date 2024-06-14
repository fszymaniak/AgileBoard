using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Exceptions;

public sealed class NoEpicReservationPolicyFound : CustomException
{
    public JobTitle JobTitle { get; }

    public NoEpicReservationPolicyFound(JobTitle jobTitle) : base($"No Epic policy for {jobTitle} has been found.")
    {
        JobTitle = jobTitle;
    }
}