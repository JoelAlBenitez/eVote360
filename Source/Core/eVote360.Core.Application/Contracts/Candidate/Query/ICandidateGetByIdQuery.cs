using eVote360.Core.Application.DTOs.Candidates;
using eVote360.Core.Domain.Common.ValidationResult;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Contracts.Candidate.Query
{
    public interface ICandidateGetByIdQuery
    {
        Task<ValidationResult<CandidateDTO>> GetByIdAsync(int candidateId, int partyId);
    }
}
