namespace AgileBoard.Application.Commands;

public record CreateFinalEpic(Guid Id, string Name, string Status, string Description, string AcceptanceCriteria, DateTimeOffset CreatedDate);