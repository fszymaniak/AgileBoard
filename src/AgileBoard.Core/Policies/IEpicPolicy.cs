using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Policies;

public interface IEpicPolicy
{
    bool CanBeApplied(JobTitle jobTitle);
    
    bool CanCreateFinalEpic();
    
    bool CanCreateDraftEpic();
    
    bool CanUpdateFinalEpic();
    
    bool CanUpdateDraftEpic();
}