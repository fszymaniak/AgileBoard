namespace AgileBoard.Core.Exceptions;

public class EpicDoesNotExistException : CustomException
{
    public EpicDoesNotExistException() : base($"Epic does not exist.")
    {
    }
}