using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Policies;

internal sealed class ProductOwnerFinalEpicCreationPolicy : IEpicPolicy
{
    public bool CanBeApplied(JobTitle jobTitle) => jobTitle == JobTitle.ProductOwner;

    public bool CanCreateFinalEpic() => true;
    
    public bool CanCreateDraftEpic() => true;
    
    public bool CanUpdateFinalEpic() => true;
    
    public bool CanUpdateDraftEpic() => true;
}
