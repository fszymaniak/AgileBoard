using AgileBoard.Core.Exceptions;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Entities;

public class FinalEpic : Epic
{
    public Status Status { get; private set; }

    public Description Description { get; private set; }

    public AcceptanceCriteria AcceptanceCriteria { get; private set; }
    
    public FinalEpic(EpicId id, Name name, Status status, Description description, AcceptanceCriteria acceptanceCriteria, Date createdDate) : base(id, name, createdDate)
    {
        Status = status;
        Description = description;
        AcceptanceCriteria = acceptanceCriteria;
    }
    
    public void ChangeStatus(Status status)
    {
        Status = status;
    }

    public void ChangeDescription(Description description)
    {
        Description = description;
    }

    public void ChangeAcceptanceCriteria(AcceptanceCriteria acceptanceCriteria)
    {
        AcceptanceCriteria = acceptanceCriteria;
    }
}
