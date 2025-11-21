using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Exceptions;

public sealed class NoEpicReservationPolicyFoundException : CustomException
{
    public JobTitle JobTitle { get; }

    public NoEpicReservationPolicyFoundException(JobTitle jobTitle) : base($"No Epic policy for {jobTitle} has been found.")
    {
        JobTitle = jobTitle;
    }
}