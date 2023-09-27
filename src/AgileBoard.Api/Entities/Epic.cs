using AgileBoard.Api.Exceptions;

namespace AgileBoard.Api.Entities;

public sealed class Epic
{
    public Guid Id { get; }
    
    public string Name { get; private set; }

    public string Status { get; private set; }

    public string Description { get; private set; }

    public string AcceptanceCriteria { get; private set; }

    public DateTime CreatedDate { get; private set; }
    
    public Epic(Guid id, string name, string status, string description, string acceptanceCriteria, DateTime createdDate)
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