namespace AgileBoard.Core.Exceptions;

public class DescriptionOverMaxCharLimit : CustomException
{
    public DescriptionOverMaxCharLimit() : base($"Description over the max characters limit.")
    {
    }
}