using AgileBoard.Application.Services.Clock;
using AgileBoard.Core.Entities;
using AgileBoard.Core.Exceptions;
using AgileBoard.Core.Policies;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.DomainServices;

public class EpicCreationService : IEpicCreationService
{
    private readonly IEnumerable<IEpicPolicy> _policies;

    public EpicCreationService(IEnumerable<IEpicPolicy> policies)
    {
        _policies = policies;
    }

    public async Task CreateEpicForRestrictedJobTitles(IEpicRepository epicRepository, Epic epic, JobTitle jobTitle)
    {
        var policy = _policies.SingleOrDefault(x => x.CanBeApplied(jobTitle));

        if (policy is null)
        {
            throw new NoEpicReservationPolicyFound(jobTitle);
        }

        if (!policy.CanCreate())
        {
            throw new CannotCreateEpicException(jobTitle);
        }
        
        await epicRepository.AddAsync(epic);
    }
}