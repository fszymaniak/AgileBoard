using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Entities;

public class DraftEpic : Epic
{
    private DraftEpic()
    {
    }
    
    public DraftEpic(EpicId id, Name name, Date createdDate) : base(id, name, createdDate)
    {
    }
}