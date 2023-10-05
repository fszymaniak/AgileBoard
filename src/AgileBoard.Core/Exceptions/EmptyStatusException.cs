namespace AgileBoard.Core.Exceptions;

public class EmptyStatusException : CustomException
{
    public EmptyStatusException() : base("Status is empty.")
    {
    }
}