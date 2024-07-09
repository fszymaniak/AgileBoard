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
    
    public void ChangeStatus(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
        {
            throw new EmptyStatusException();
        }

        Status = status;
    }
    
    public void ChangeDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new EmptyDescriptionException();
        }

        Description = description;
    }
    
    public void ChangeAcceptanceCriteria(string acceptanceCriteria)
    {
        if (string.IsNullOrWhiteSpace(acceptanceCriteria))
        {
            throw new EmptyAcceptanceCriteriaException();
        }

        AcceptanceCriteria = acceptanceCriteria;
    }
}
