using AgileBoard.Core.Exceptions;

namespace AgileBoard.Core.ValueObjects;

public sealed record EpicId
{
    public Guid Value { get; }

    public EpicId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static EpicId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(EpicId userStoryId) => userStoryId.Value;

    public static implicit operator EpicId(Guid value) => new(value);
}