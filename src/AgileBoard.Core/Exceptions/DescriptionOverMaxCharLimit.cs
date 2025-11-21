namespace AgileBoard.Core.Exceptions;

public class DescriptionOverMaxCharLimitException : CustomException
{
    public DescriptionOverMaxCharLimitException() : base($"Description over the max characters limit.")
    {
    }
}