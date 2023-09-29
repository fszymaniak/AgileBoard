using AgileBoard.Api.Exceptions;

namespace AgileBoard.Api.ValueObjects;

public sealed record AcceptanceCriteria(string Value)
{
    public string Value { get; } = Value ?? throw new EmptyAcceptanceCriteriaException();

    public static implicit operator string(AcceptanceCriteria acceptanceCriteria) => acceptanceCriteria.Value;

    public static implicit operator AcceptanceCriteria(string value) => new(value);
}