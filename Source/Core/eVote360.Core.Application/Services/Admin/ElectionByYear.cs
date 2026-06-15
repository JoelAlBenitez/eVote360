using eVote360.Core.Application.Contracts.Admin.Query;
using eVote360.Core.Application.DTOs.Admin;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.AdminManager;
using eVote360.Core.Domain.Validators.Admin;

namespace eVote360.Core.Application.Services.Admin
{
    public class ElectionByYear : IElectionByYearQuery
    {

        private readonly IAdminManagerRepository _adminManagerRepository;
        private readonly IAdminValidator _adminValidator;
        public ElectionByYear(IAdminManagerRepository adminManagerRepository,
            IAdminValidator adminValidator
            )
        {
            _adminManagerRepository = adminManagerRepository;
            _adminValidator = adminValidator;
        }


        public async Task<ValidationResult<IReadOnlyCollection<ElectoralSummaryDto>>> GetRegisterAsync(DateTime year)
        {
            try
            {
                var validate = await _adminValidator.ValidateElectionByYear(year.Year);
                if (validate != null) return (ValidationResult<IReadOnlyCollection<ElectoralSummaryDto>>)validate;
                var dtoList = new List<ElectoralSummaryDto>();
                var list = await _adminManagerRepository.ElectionByYearAsync(year);

                foreach (var item in list)
                {

                    var dto = new ElectoralSummaryDto
                    {
                        DateRealized
                        = item.DateRealized,
                        NameElection = item.NameElection,
                        NumberCandidactesParticipating = item.NumberCandidactesParticipating,
                        NumberCitizenParticipating = item.NumberCitizenParticipating,
                        NumberParticipatingMatches = item.NumberParticipatingMatches,
                    };
                    dtoList.Add(dto);
                }
              
                return ValidationResult<IReadOnlyCollection < ElectoralSummaryDto >>.Success(dtoList);
            }
            catch (Exception ex)
            {
                var errors = new List<Error>();
                errors.Add(new Error("Ha ocurrido un error inesperado", ex.Message));
                return ValidationResult< IReadOnlyCollection<ElectoralSummaryDto>>.Failure(errors);
            }
        }
    }
}
