using eVote360.Core.Domain.Entities.PoliticalParty;
using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Domain.Validators.PoliticalPartyValidator
{
    public interface IPoliticalPartyValidator
    {
        Task<ValidationResult> ValidateCreate(PoliticalParty party);
        Task<ValidationResult> ValidateUpdate(PoliticalParty party);
    }
}
