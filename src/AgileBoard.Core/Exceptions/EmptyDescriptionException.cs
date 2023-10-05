namespace AgileBoard.Core.Exceptions;

public class EmptyDescriptionException : CustomException
{
    public EmptyDescriptionException() : base("Description is empty.")
    {
    }
}