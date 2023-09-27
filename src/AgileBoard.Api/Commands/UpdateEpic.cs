namespace AgileBoard.Api.Commands;

public record UpdateEpic(Guid EpicId, string Name, string Status, string Description, string AcceptanceCriteria);