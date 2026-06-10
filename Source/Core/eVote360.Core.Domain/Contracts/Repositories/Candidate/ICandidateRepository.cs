using eVote360.Core.Domain.Contracts.Repositories.BaseRepository;

namespace eVote360.Core.Domain.Contracts.Repositories.Candidate
{
    public interface ICandidateRepository : IBaseRepository<Entities.Candidate.Candidate, int>
    {
        Task<IEnumerable<Entities.Candidate.Candidate>> GetAllByPartyIdAsync(int partyId);
    }
}
