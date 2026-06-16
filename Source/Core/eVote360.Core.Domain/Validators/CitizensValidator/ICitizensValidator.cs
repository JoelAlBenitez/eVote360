using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Entities.Citizens;

namespace eVote360.Core.Domain.Validators.CitizensValidator
{
    public interface ICitizensValidator
    {
        Task<ValidationResult> CreateCitizen(Citizen citizen);
        Task<ValidationResult> UpdateCitizen(Citizen citizen);
        Task<ValidationResult> ActiveCitizen(Guid Id, string Identification);
        Task<ValidationResult> DesactiveCitizen(Guid Id, string Identification);

    }
}
