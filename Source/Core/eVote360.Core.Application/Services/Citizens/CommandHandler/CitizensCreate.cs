using eVote360.Core.Application.Contracts.Citizens.Command;
using eVote360.Core.Domain.Entities.Citizens;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.Citizens;
using eVote360.Core.Domain.Validators.CitizensValidator;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Application.DTOs.Citizens;
using eVote360.Core.Domain.Settings.ValueObjects.Identifications;
using eVote360.Core.Domain.Settings.ValueObjects.Emails;

namespace eVote360.Core.Application.Services.Citizens.CommandHandler
{
    public  sealed class CitizensCreate : ICitizensCreateCommand
    {
       
        private readonly ICitizenRepository _citizenRepository;
        private readonly ICitizensValidator _citizensValidator;
        private List<Error> _errors = new List<Error>();

        public CitizensCreate(ICitizenRepository citizenRepository,
            ICitizensValidator citizensValidator
            
            ) { 
            _citizenRepository = citizenRepository;
            _citizensValidator = citizensValidator;
        }

        public async Task<ValidationResult> CreateAsync(CitizensDto citizen)
        {
            try
            {
                var identification = new IdentificationN(citizen.Identification);
                var email = new Email(citizen.Email);
                var citizenEntitie = new Citizen { 
                    IdentificationNumber = identification,
                    Name = citizen.Name,
                    LastName = citizen.LastName,
                    Email = email,
                    State = citizen.State,   
                    CreateAt = DateTimeOffset.Now,
                    CreateUserId = 0 //cambiar por el id de la cookie session
                };

                var validate = await _citizensValidator.CreateCitizen(citizenEntitie);
                if (validate != null) return validate;

                var create  = await _citizenRepository.CreateEntiteAsync(citizenEntitie);
             
                if(!create)
                {
                    _errors.Add(new Error("Ha ocurrido un error", "Ha ocurrido un error en el procamiento del ciudadano"));
                    return ValidationResult.Failure(_errors);
                }
                return ValidationResult.Success();

            }
            catch (Exception ex) {

                _errors.Add(new Error("Ha ocurrido un error", ex.Message ));
                return ValidationResult.Failure(_errors);
            }
        }
    }
}
