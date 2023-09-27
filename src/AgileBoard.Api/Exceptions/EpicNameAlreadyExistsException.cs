using AgileBoard.Api.Entities;

namespace AgileBoard.Api.Exceptions;

public class EpicNameAlreadyExistsException : CustomException
{
    public EpicNameAlreadyExistsException(Epic epic) : base($"Epic name: {epic.Name} already exists.")
    {
    }
}