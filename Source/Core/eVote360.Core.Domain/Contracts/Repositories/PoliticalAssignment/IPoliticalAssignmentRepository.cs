using eVote360.Core.Domain.Contracts.Repositories.BaseRepository;
using eVote360.Core.Domain.Entities.PoliticalAssignment;
using System.Collections;


namespace eVote360.Core.Domain.Contracts.Repositories.PoliticalAssignment
{
    public interface IPoliticalAssignmentRepository : IBaseRepository<Entities.PoliticalAssignment.PoliticalAssignment,int>
    {
        Task<bool> HasAlreadyAssignment (int politicalLeaderId, int politicalPartyId);

        Task<IEnumerable> GetAllLeadersFromParty (int politicalPartyId);


    }
}
