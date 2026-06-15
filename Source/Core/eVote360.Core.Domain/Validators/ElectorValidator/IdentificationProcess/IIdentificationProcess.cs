using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Domain.Validators.ElectorValidator.IdentificationProcess
{
    public  interface IIdentificationProcess
    {
        Task<ValidationResult> ValidateEnteredIdentification(string Identification);
        Task<ValidationResult> ValidateComparadIdentificationByImg(string IdentificationImg, string IdentificationEntered);

    }
}
