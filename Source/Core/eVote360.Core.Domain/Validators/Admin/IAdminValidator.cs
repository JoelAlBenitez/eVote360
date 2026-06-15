using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Domain.Validators.Admin
{
    public interface IAdminValidator
    {
        Task<ValidationResult> ValidateElectionQuery();
        Task<ValidationResult> ValidateElectionByYear(int year);
    }
}
