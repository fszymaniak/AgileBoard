using AgileBoard.Core.Exceptions;

namespace AgileBoard.Core.ValueObjects;

public sealed record Status
{
    public string Value { get; }

    public Status(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyStatusException();
        }

        Value = value;
    }

    public static implicit operator string(Status status) => status.Value;

    public static implicit operator Status(string value) => new(value);
}
