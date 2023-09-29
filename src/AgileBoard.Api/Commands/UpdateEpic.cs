namespace AgileBoard.Api.Commands;

public record UpdateEpic(Guid Id, string Name, string Status, string Description, string AcceptanceCriteria);