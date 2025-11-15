using AgileBoard.Core.Exceptions;

namespace AgileBoard.Core.ValueObjects;

public sealed record AcceptanceCriteria
{
    public string Value { get; }

    public AcceptanceCriteria(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyAcceptanceCriteriaException();
        }

        Value = value;
    }

    public static implicit operator string(AcceptanceCriteria acceptanceCriteria) => acceptanceCriteria.Value;

    public static implicit operator AcceptanceCriteria(string value) => new(value);
}
