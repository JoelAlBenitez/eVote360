using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Domain.Validators.ElectionValidator
{
    public interface IElectionValidator
    {
        Task<ValidationResult> ValidateElection(Entities.Election.Election election);
    }
}
