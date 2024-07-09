namespace AgileBoard.Application.Commands;

public record CreateDraftEpic(Guid Id, string Name, DateTimeOffset CreatedDate);