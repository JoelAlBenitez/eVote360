using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Entities.Citizens;

namespace eVote360.Core.Domain.Validators.CitizensValidator
{
    public class CitizensValidator : ICitizensValidator
    {


        public Task<ValidationResult> ActiveCitizen(Guid Id, string Identification)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> CreateCitizen(Citizen citizen)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> DesactiveCitizen(Guid Id, string Identification)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> UpdateCitizen(Citizen citizen)
        {
            throw new NotImplementedException();
        }
    }
}
