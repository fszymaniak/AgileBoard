namespace AgileBoard.Application.Commands;

public record UpdateFinalEpic(Guid Id, string Name, string Status, string Description, string AcceptanceCriteria);