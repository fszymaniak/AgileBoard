using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Policies;

internal sealed class ProductOwnerEpicCreationPolicy : IEpicPolicy
{
    public bool CanBeApplied(JobTitle jobTitle) => jobTitle == JobTitle.ProductOwner;

    public bool CanCreate() => true;
}
