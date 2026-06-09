using eVote360.Core.Application.DTOs.Candidates;
using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Contracts.Candidate
{
    public interface ICandidateService
    {


        Task<ValidationResult> CreateCandidateAsync(CreateCandidateDto dto, int PartyId);
        Task<ValidationResult> UpdateCandidateAsync(UpdateCandidateDto dto, int PartyId);
        Task<ValidationResult> ChangeStateAsync(int candidateId, int PartyId);
        Task<CandidateDTO> GetByIdAsync(int candidateId, int partyId);
        Task<IEnumerable<CandidateDTO>> GetAllPartyAsync (int PartyId);


    }
}
