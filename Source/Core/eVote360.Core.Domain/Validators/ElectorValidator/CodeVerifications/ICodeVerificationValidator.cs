using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Domain.Validators.ElectorValidator.CodeVerifications
{
    public interface ICodeVerificationValidator
    {
        Task<ValidationResult> VerificationCode(Guid IdCitizens, int IdElection, int Code);

    }
}
