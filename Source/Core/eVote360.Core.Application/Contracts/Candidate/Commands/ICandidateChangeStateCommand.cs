using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Contracts.Candidate.Commands
{
    public interface ICandidateChangeStateCommand
    {
        Task<ValidationResult> ChangeStateAsync(int candidateId, int PartyId);
    }
}
