namespace AgileBoard.Api.Exceptions;

public class EmptyStatusException : CustomException
{
    public EmptyStatusException() : base("Status is empty.")
    {
    }
}