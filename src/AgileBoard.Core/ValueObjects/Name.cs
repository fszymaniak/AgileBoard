using AgileBoard.Core.Exceptions;

namespace AgileBoard.Core.ValueObjects;

public sealed record Name
{
    public string Value { get; }

    public Name(string value)
    {
        Value = value ?? throw new EmptyNameException();
    }

    public static implicit operator string(Name name) => name.Value;

    public static implicit operator Name(string value) => new(value);
}