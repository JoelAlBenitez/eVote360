using eVote360.Core.Application.DTOs.Candidates;

namespace eVote360.Core.Application.Contracts.Candidate.Query
{
    public interface ICandidateGetByIdQuery
    {
        Task<CandidateDTO> GetByIdAsync(int candidateId, int partyId);
    }
}
