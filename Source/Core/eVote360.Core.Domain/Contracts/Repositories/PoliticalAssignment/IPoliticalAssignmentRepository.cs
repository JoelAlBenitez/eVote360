using eVote360.Core.Domain.Contracts.Repositories.BaseRepository;
using System.Collections;
using PoliticalAssignment = eVote360.Core.Domain.Entities.PoliticalAssignment.PoliticalAssignment;
using AssignmentEntity = eVote360.Core.Domain.Entities.PoliticalAssignment.PoliticalAssignment;


namespace eVote360.Core.Domain.Contracts.Repositories.PoliticalAssignment
{
    public interface IPoliticalAssignmentRepository : IBaseRepository<Entities.PoliticalAssignment.PoliticalAssignment,int>
    {
        Task<bool> HasAlreadyAssignmentAsync (int politicalLeaderId, int politicalPartyId);

        Task<IEnumerable> GetAllLeadersFromPartyAsync (int politicalPartyId);

        Task<IReadOnlyCollection<AssignmentEntity>> GetAllAsync();
    }
}
