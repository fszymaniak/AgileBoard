using AgileBoard.Api.Exceptions;
using AgileBoard.Api.ValueObjects;

namespace AgileBoard.Api.Entities;

public sealed class Epic
{
    public EpicId Id { get; }
    
    public Name Name { get; private set; }

    public Status Status { get; private set; }

    public Description Description { get; private set; }

    public AcceptanceCriteria AcceptanceCriteria { get; private set; }

    public Date CreatedDate { get; private set; }
    
    public Epic(EpicId id, Name name, Status status, Description description, AcceptanceCriteria acceptanceCriteria, Date createdDate)
    {
        Id = id;
        Name = name;
        Status = status;
        Description = description;
        AcceptanceCriteria = acceptanceCriteria;
        CreatedDate = createdDate;
    }

    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new EmptyNameException();
        }

        Name = name;
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
            throw new InvalidDescriptionException(description);
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