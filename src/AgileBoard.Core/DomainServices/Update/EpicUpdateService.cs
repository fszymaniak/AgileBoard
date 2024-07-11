using AgileBoard.Core.Entities;
using AgileBoard.Core.Exceptions;
using AgileBoard.Core.Policies;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.DomainServices.Update;

public class EpicUpdateService : IEpicUpdateService
{
    private readonly IEnumerable<IEpicPolicy> _policies;

    public EpicUpdateService(IEnumerable<IEpicPolicy> policies)
    {
        _policies = policies;
    }

    public async Task UpdateFinalEpicForRestrictedJobTitles(IEpicRepository epicRepository, FinalEpic finalEpic, string name, string status, string description, string acceptanceCriteria, JobTitle jobTitle)
    {
        var policy = _policies.SingleOrDefault(x => x.CanBeApplied(jobTitle));

        if (policy is null)
        {
            throw new NoEpicReservationPolicyFound(jobTitle);
        }

        if (!policy.CanUpdateFinalEpic())
        {
            throw new CannotCreateFinalEpicException(jobTitle);
        }
        
        finalEpic.ChangeName(name);
        finalEpic.ChangeStatus(status);
        finalEpic.ChangeDescription(description);
        finalEpic.ChangeAcceptanceCriteria(acceptanceCriteria);
        
        await epicRepository.UpdateEpicAsync(finalEpic);
    }
    
    public async Task UpdateDraftEpicForRestrictedJobTitles(IEpicRepository epicRepository, DraftEpic draftEpic, string name, JobTitle jobTitle)
    {
        var policy = _policies.SingleOrDefault(x => x.CanBeApplied(jobTitle));

        if (policy is null)
        {
            throw new NoEpicReservationPolicyFound(jobTitle);
        }

        if (!policy.CanUpdateDraftEpic())
        {
            throw new CannotCreateFinalEpicException(jobTitle);
        }
        
        draftEpic.ChangeName(name);
        
        await epicRepository.UpdateEpicAsync(draftEpic);
    }
}