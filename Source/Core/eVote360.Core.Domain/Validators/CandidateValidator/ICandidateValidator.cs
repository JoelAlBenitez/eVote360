using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Domain.Validators.CandidateValidator
{
    public interface ICandidateValidator
    {
        Task<ValidationResult> ValidateCreateAsync(int partyId);
       
        Task<ValidationResult> ValidateChangeStateAsync(int candidateId);
        Task<ValidationResult> ValidateUpdateAsync(int candidateId, string name, string lastName, int partyId);
    }
}
