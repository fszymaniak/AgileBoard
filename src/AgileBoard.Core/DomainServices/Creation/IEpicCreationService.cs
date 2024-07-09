using AgileBoard.Core.Entities;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.DomainServices.Creation;

public interface IEpicCreationService
{
    Task CreateFinalEpicForRestrictedJobTitles(IFinalEpicRepository epicRepository, FinalEpic epic, JobTitle jobTitle);
    
    Task CreateDraftEpicForRestrictedJobTitles(IDraftEpicRepository epicRepository, DraftEpic epic, JobTitle jobTitle);
}