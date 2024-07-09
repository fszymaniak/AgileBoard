using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Policies;

internal sealed class ScrumMasterFinalEpicCreationPolicy : IEpicPolicy
{
    public bool CanBeApplied(JobTitle jobTitle) => jobTitle == JobTitle.ScrumMaster;

    public bool CanCreateFinalEpic() => false;
    
    public bool CanCreateDraftEpic() => true;
    
    public bool CanUpdateFinalEpic() => false;
    
    public bool CanUpdateDraftEpic() => true;
}