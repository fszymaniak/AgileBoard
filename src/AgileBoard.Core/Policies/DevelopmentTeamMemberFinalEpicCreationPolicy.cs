using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Policies;

internal sealed class DevelopmentTeamMemberFinalEpicCreationPolicy : IEpicPolicy
{
    public bool CanBeApplied(JobTitle jobTitle) => jobTitle == JobTitle.DevelopmentTeamMember;

    public bool CanCreateFinalEpic() => false;
    
    public bool CanCreateDraftEpic() => true;
    
    public bool CanUpdateFinalEpic() => false;
    
    public bool CanUpdateDraftEpic() => true;
}
