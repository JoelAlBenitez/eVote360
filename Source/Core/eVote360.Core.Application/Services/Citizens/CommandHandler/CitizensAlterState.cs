using eVote360.Core.Application.Contracts.Citizens.Command;
using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.Citizens;
using eVote360.Core.Domain.Validators.CitizensValidator;

namespace eVote360.Core.Application.Services.Citizens.CommandHandler
{
    public sealed class CitizensAlterState : ICitizensAlterStateCommand
    {

        private readonly ICitizenRepository _citizenRepository;
        private readonly ICitizensValidator _citizensValidator;
      
        private List<Error> _errors = new List<Error>();

        public CitizensAlterState(ICitizenRepository citizenRepository, 
            ICitizensValidator citizensValidator)
        {
            _citizenRepository = citizenRepository;
            _citizensValidator = citizensValidator;
          
        }

        public async Task<ValidationResult> AlterStateAsync(Guid Id)
        {
            try
            {
               var citizen = await _citizenRepository.GetByIdEntitie(Id);
               if(citizen == null)
                {
                    _errors.Add(CitizenErrors.NoExtisCitizen);
                    return ValidationResult.Failure(_errors);
                }

                if (citizen.State) // currently active → deactivate
                {
                    var validate = await _citizensValidator.DesactiveCitizen(Id, citizen.IdentificationNumber.Value);
                    if (!validate.IsValid) return validate;
                    var active = await _citizenRepository.AlterState(Id, false);
                    if (active) return ValidationResult.Success();
                    _errors.Add(new Error("Ha ocurrido un error inesperado", "Ha ocurrido un error en la alteración del registro, intente lo de nuevo"));
                    return ValidationResult.Failure(_errors);
                }
                else // currently inactive → activate
                {
                    var validate = await _citizensValidator.ActiveCitizen(Id, citizen.IdentificationNumber.Value);
                    if (!validate.IsValid) return validate;
                    var active = await _citizenRepository.AlterState(Id, true);
                    if (active) return ValidationResult.Success();
                    _errors.Add(new Error("Ha ocurrido un error inesperado", "Ha ocurrido un error en la alteración del registro, intente lo de nuevo"));
                    return ValidationResult.Failure(_errors);

                }
                   
            }
            catch (Exception ex) {

                _errors.Add(new Error("Ha ocurrido un error inesperado", ex.Message));
                return ValidationResult.Failure(_errors);
            }
        }
    }
}
