using eVote360.Core.Application.DTOs.Candidates;
using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Contracts.Candidate.Commands
{
    public interface ICandidateCreateCommand
    {
        Task<ValidationResult> CreateCandidateAsync(CreateCandidateDto dto, int PartyId);
    }
}
