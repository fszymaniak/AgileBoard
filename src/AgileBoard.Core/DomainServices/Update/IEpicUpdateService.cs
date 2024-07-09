using AgileBoard.Core.Entities;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.DomainServices.Update;

public interface IEpicUpdateService
{
    Task UpdateFinalEpicForRestrictedJobTitles(IFinalEpicRepository epicRepository, FinalEpic finalEpic,
        string name, string status, string description, string acceptanceCriteria, JobTitle jobTitle);
    
    Task UpdateDraftEpicForRestrictedJobTitles(IDraftEpicRepository epicRepository, DraftEpic finalEpic,
        string name, JobTitle jobTitle);
}