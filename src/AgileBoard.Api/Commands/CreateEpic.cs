namespace AgileBoard.Api.Commands;

public record CreateEpic(Guid Id, string Name, string Status, string Description, string AcceptanceCriteria, DateTimeOffset CreatedDate);