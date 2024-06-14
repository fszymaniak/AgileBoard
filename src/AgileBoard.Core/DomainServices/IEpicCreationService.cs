using AgileBoard.Core.Entities;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.DomainServices;

public interface IEpicCreationService
{
    Task CreateEpicForRestrictedJobTitles(IEpicRepository epicRepository, Epic epic, JobTitle jobTitle);
}