using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Policies;

public class BusinessAnalystEpicCreationPolicy : IEpicPolicy
{
    public bool CanBeApplied(JobTitle jobTitle) => jobTitle == JobTitle.BusinessAnalyst;

    public bool CanCreateFinalEpic() => true;
    
    public bool CanCreateDraftEpic() => true;
    
    public bool CanUpdateFinalEpic() => true;
    
    public bool CanUpdateDraftEpic() => true;
}