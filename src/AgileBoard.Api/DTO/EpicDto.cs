namespace AgileBoard.Api.DTO;

public class EpicDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public string Status { get; set; }

    public string Description { get; set; }

    public string AcceptanceCriteria { get; set; }

    public DateTime CreatedDate { get; set; }
}