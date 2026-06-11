using eVote360.Core.Application.Contracts.Citizens.Command;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Contracts.Repositories.Citizens;
using eVote360.Core.Domain.Entities.Citizens;
using eVote360.Core.Domain.Validators.CitizensValidator;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Settings.ValueObjects.Identifications;
using eVote360.Core.Application.DTOs.Citizens;
using eVote360.Core.Domain.Settings.ValueObjects.Emails;
namespace eVote360.Core.Application.Services.Citizens.CommandHandler
{
    public class CitizensUpdate : ICitizensEditCommand
    {

        private readonly ICitizenRepository _citizenRepository;
        private readonly ICitizensValidator _citizensValidator;
        private List<Error> _errors = new List<Error>();

        public CitizensUpdate(ICitizenRepository citizenRepository,
            ICitizensValidator citizensValidator)
        {
            _citizenRepository = citizenRepository;
            _citizensValidator = citizensValidator;
        }

        public async Task<ValidationResult> UpdateAsync(CitizensDto citizen)
        {
            try
            {
                var identification = new IdentificationN(citizen.Identification);
                var email = new Email(citizen.Email);
                var citizenE = new Citizen
                {
                    Id = citizen.Id,
                    IdentificationNumber = identification,
                    Email = email,
                    Name = citizen.Name,
                    LastName = citizen.LastName,
                    State = citizen.State,
                    UpdateAt = DateTimeOffset.Now,
                    UpdateUserId = 0 //cambiar por el obtenido de la cookies session

                };
                var validate = await _citizensValidator.UpdateCitizen(citizenE);
                if (validate != null) return validate;

                var update = await _citizenRepository.UpdateEntitieAsync(citizenE);
                if (!update)
                {
                    _errors.Add(new Error("Ha ocurrido un error inesperado", "Error al editar la información del ciudadano."));
                    return ValidationResult.Failure(_errors);
                }
                return ValidationResult.Success();
            }
            catch (Exception ex)
            {
                _errors.Add(new Error("Has ocurrido un error inesperado", ex.Message));
                return ValidationResult.Failure(_errors);
            }
        }
    }
}
