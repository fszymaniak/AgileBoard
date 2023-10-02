using AgileBoard.Api.Exceptions;

namespace AgileBoard.Api.ValueObjects;

public sealed record Description
{
    public string Value { get; }

    public Description(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyDescriptionException();
        }
        
        if (value.Length > 500)
        {
            throw new DescriptionOverMaxCharLimit();
        }

        Value = value;
    }

    public static implicit operator string(Description description) => description.Value;

    public static implicit operator Description(string value) => new(value);
}