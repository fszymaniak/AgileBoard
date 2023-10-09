namespace AgileBoard.Core.Exceptions;

public class EpicDoesNotExist : CustomException
{
    public EpicDoesNotExist() : base($"Epic does not exist.")
    {
    }
}