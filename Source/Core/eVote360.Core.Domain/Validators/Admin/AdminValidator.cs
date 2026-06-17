using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.ServiceValidates.Admin;

namespace eVote360.Core.Domain.Validators.Admin
{
    public class AdminValidator : IAdminValidator
    {

        private readonly IAdminFunctionalitysValidate _adminFunctionalitysValidate;
        private List<Error> _errors = new List<Error>();

        public AdminValidator(IAdminFunctionalitysValidate adminFunctionalitysValidate)
        {
            _adminFunctionalitysValidate = adminFunctionalitysValidate;
            
        }

        public async Task<ValidationResult> ValidateElectionByYear(int year)
        {
            var existeElectionByYear = await _adminFunctionalitysValidate.ExistElectionByYear(year);
            if (!existeElectionByYear)
            {
                _errors.Add(AdminError.YearNoValid);
                return ValidationResult.Failure(_errors);
            }
            return ValidationResult.Success();
        }


        public async Task<ValidationResult> ValidateElectionQuery()
        {
            var existElection = await _adminFunctionalitysValidate.ExisteElections();
            if (!existElection)
            {
                _errors.Add(AdminError.NoWxisteElection);
                return ValidationResult.Failure(_errors);
            }

            return ValidationResult.Success();
        }
    }
}
