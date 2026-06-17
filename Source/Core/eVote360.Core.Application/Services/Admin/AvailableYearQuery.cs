using eVote360.Core.Application.Contracts.Admin.Query;
using eVote360.Core.Application.DTOs.Admin;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.AdminManager;
using eVote360.Core.Domain.Validators.Admin;
using System.Collections.Generic;

namespace eVote360.Core.Application.Services.Admin
{
    public class AvailableYearQuery : IAvailableYearsQuery
    {

        private readonly IAdminManagerRepository _adminManagerRepository;
        private readonly IAdminValidator _adminValidator;

        public AvailableYearQuery(IAdminManagerRepository adminManagerRepository,
            IAdminValidator adminValidator
            )
        {
            _adminManagerRepository = adminManagerRepository;
            _adminValidator = adminValidator;
        }

        

        public async Task<ValidationResult<IReadOnlyCollection<AdminDate>>> AvailableYearAsync()
        {
            try
            {

                var validate = await _adminValidator.ValidateElectionQuery();
                if (!validate.IsValid) return ValidationResult<IReadOnlyCollection<AdminDate>>.Failure(validate.errors.ToList());

                var list = await _adminManagerRepository.GetYears();        
                var dtosList = new List<AdminDate>();
                foreach (var date in list)
                {

                    var dto = new AdminDate { YearElection = date };
                    dtosList.Add(dto);
                }
              
                return ValidationResult<IReadOnlyCollection <AdminDate>>.Success(dtosList);
            }
            catch (Exception ex)
            {
                var errors = new List<Error>();
                errors.Add(new Error("Ha ocurrido un error", ex.Message));
                return ValidationResult<IReadOnlyCollection<AdminDate>>.Failure(errors);
            }
        }
    }
}
