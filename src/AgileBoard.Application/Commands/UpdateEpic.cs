namespace AgileBoard.Application.Commands;

public record UpdateEpic(Guid Id, string Name, string Status, string Description, string AcceptanceCriteria);