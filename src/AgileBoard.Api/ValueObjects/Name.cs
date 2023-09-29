using AgileBoard.Api.Exceptions;

namespace AgileBoard.Api.ValueObjects;

public sealed record Name(string Value)
{
    public string Value { get; } = Value ?? throw new EmptyNameException();

    public static implicit operator string(Name name) => name.Value;

    public static implicit operator Name(string value) => new(value);
}