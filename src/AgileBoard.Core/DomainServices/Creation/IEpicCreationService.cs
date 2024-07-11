using AgileBoard.Core.Entities;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.DomainServices.Creation;

public interface IEpicCreationService
{
    Task CreateFinalEpicForRestrictedJobTitles(IEpicRepository epicRepository, FinalEpic epic, JobTitle jobTitle);
    
    Task CreateDraftEpicForRestrictedJobTitles(IEpicRepository epicRepository, DraftEpic epic, JobTitle jobTitle);
}