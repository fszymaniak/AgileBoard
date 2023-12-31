using AgileBoard.Core.Exceptions;

namespace AgileBoard.Core.ValueObjects;

public sealed record Status(string Value)
{
    public string Value { get; } = Value ?? throw new EmptyStatusException();

    public static implicit operator string(Status status) => status.Value;

    public static implicit operator Status(string value) => new(value);
}