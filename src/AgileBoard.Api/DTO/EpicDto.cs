using AgileBoard.Api.ValueObjects;

namespace AgileBoard.Api.DTO;

public class EpicDto
{
    public EpicId Id { get; set; }
    
    public Name Name { get; set; }

    public Status Status { get; set; }

    public Description Description { get; set; }

    public AcceptanceCriteria AcceptanceCriteria { get; set; }

    public Date CreatedDate { get; set; }
}