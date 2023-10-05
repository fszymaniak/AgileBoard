namespace AgileBoard.Core.Exceptions;

public class EmptyAcceptanceCriteriaException : CustomException
{
    public EmptyAcceptanceCriteriaException() : base("Acceptance Criteria is empty.")
    {
    }
}