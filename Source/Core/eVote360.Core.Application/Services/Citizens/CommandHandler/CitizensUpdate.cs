using eVote360.Core.Application.Contracts.Citizens.Command;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Contracts.Repositories.Citizens;
using eVote360.Core.Domain.Entities.Citizens;
using eVote360.Core.Domain.Validators.CitizensValidator;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Settings.ValueObjects.Identifications;
using eVote360.Core.Application.DTOs.Citizens;
using eVote360.Core.Domain.Settings.ValueObjects.Emails;
using eVote360.Core.Application.Contracts.Authentication.Command;
namespace eVote360.Core.Application.Services.Citizens.CommandHandler
{
    public sealed class CitizensUpdate : ICitizensEditCommand
    {

        private readonly ICitizenRepository _citizenRepository;
        private readonly ICitizensValidator _citizensValidator;
        private readonly ISessionUser _sessionUser;
        private List<Error> _errors = new List<Error>();

        public CitizensUpdate(ICitizenRepository citizenRepository,
            ICitizensValidator citizensValidator,
            ISessionUser sessionUser
            )
        {
            _citizenRepository = citizenRepository;
            _citizensValidator = citizensValidator;
            _sessionUser = sessionUser;

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
                    UpdateUserId = _sessionUser.GetUserId()

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
