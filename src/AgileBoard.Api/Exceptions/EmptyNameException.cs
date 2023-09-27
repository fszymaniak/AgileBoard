namespace AgileBoard.Api.Exceptions;

public class EmptyNameException : CustomException
{
    public EmptyNameException() : base("Name is empty")
    {
    }
}