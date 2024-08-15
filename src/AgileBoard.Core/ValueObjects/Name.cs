using AgileBoard.Core.Exceptions;

namespace AgileBoard.Core.ValueObjects;

public sealed record Name(string Value)
{
    public string Value { get; set; } = Value ?? throw new EmptyNameException();

    public static implicit operator string(Name name) => name.Value;

    public static implicit operator Name(string value) => new(value);
}