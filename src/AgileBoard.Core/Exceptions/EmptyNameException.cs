namespace AgileBoard.Core.Exceptions;

public class EmptyNameException : CustomException
{
    public EmptyNameException() : base("Name is empty.")
    {
    }
}