using AgileBoard.Core.Entities;

namespace AgileBoard.Core.Exceptions;

public class EpicNameAlreadyExistsException : CustomException
{
    public EpicNameAlreadyExistsException(Epic epic) : base($"Epic name: {epic.Name} already exists.")
    {
    }
}