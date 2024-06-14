using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Policies;

internal sealed class DevelopmentTeamMemberEpicCreationPolicy : IEpicPolicy
{
    public bool CanBeApplied(JobTitle jobTitle) => jobTitle == JobTitle.DevelopmentTeamMember;

    public bool CanCreate() => false;
}
