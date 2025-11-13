using AgileBoard.Core.Entities;
using AgileBoard.Core.Exceptions;
using AgileBoard.Core.Policies;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.DomainServices.Creation;

public class EpicCreationService : IEpicCreationService
{
    private readonly IEnumerable<IEpicPolicy> _policies;

    public EpicCreationService(IEnumerable<IEpicPolicy> policies)
    {
        _policies = policies;
    }

    public async Task CreateFinalEpicForRestrictedJobTitles(IEpicRepository epicRepository, FinalEpic epic, JobTitle jobTitle)
    {
        var policy = _policies.SingleOrDefault(x => x.CanBeApplied(jobTitle));

        if (policy is null)
        {
            throw new NoEpicReservationPolicyFound(jobTitle);
        }

        if (!policy.CanCreateFinalEpic())
        {
            throw new CannotCreateFinalEpicException(jobTitle);
        }
        
        await epicRepository.AddEpicAsync(epic);
    }
    
    public async Task CreateDraftEpicForRestrictedJobTitles(IEpicRepository epicRepository, DraftEpic epic, JobTitle jobTitle)
    {
        var policy = _policies.SingleOrDefault(x => x.CanBeApplied(jobTitle));

        if (policy is null)
        {
            throw new NoEpicReservationPolicyFound(jobTitle);
        }

        if (!policy.CanCreateDraftEpic())
        {
            throw new CannotCreateDraftEpicException(jobTitle);
        }
        
        await epicRepository.AddEpicAsync(epic);
    }
}