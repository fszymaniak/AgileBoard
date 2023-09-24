namespace AgileBoard.Api.Models;

public sealed class Epic
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Status { get; set; }

    public string Description { get; set; }

    public string AcceptanceCriteria { get; set; }

    public DateTime CreatedDate { get; set; }
}