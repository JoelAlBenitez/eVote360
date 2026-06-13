using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Entities.PoliticalAlliances;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Validators.PoliticalAlliancesValidator
{
    public interface IAllianceValidator
    {
        Task<ValidationResult> ValidateCreateRequestAsync(int requestingPartyId, int receivingPartyId);
        Task<ValidationResult> ValidateAcceptRequestAsync(PoliticalAlliances alliance, int currentUserIdParty);
        Task<ValidationResult> ValidateRejectRequestAsync(PoliticalAlliances alliance, int currentUserIdParty);
        Task<ValidationResult> ValidateDeleteRequestAsync(PoliticalAlliances alliance, int currentUserIdParty);
        Task<ValidationResult> ValidateDeleteActiveAllianceAsync(PoliticalAlliances alliance, int currentUserIdParty);
    }
}
