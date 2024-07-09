using AgileBoard.Core.Exceptions;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Entities;

public abstract class Epic
{
    public EpicId Id { get; private set; }
    
    public Name Name { get; private set; }

    public Date CreatedDate { get; private set; }

    protected Epic()
    {
    }
    
    public Epic(EpicId id, Name name, Date createdDate)
    {
        Id = id;
        Name = name;
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
}