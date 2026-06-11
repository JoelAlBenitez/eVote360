using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.ServiceValidates.Citizens;
using eVote360.Core.Domain.Entities.Citizens;
using System.Text.RegularExpressions;

namespace eVote360.Core.Domain.Validators.CitizensValidator
{
    public class CitizensValidator : ICitizensValidator
    {

        private readonly ICitizensServiceValidate _citizensServiceValidate;
        private List<Error> _errors = new List<Error>();

        public CitizensValidator(ICitizensServiceValidate citizensServiceValidate)
        {
            _citizensServiceValidate = citizensServiceValidate;
        }
        private Error ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)
                || name.Trim().Length > 40 ||
                !Regex.IsMatch("^[a-zA-Z\\s]+$", name.Trim()
                )) return  CitizenErrors.NameNoValid;

            return null!;
        }

        private Error ValidateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName)
                || lastName.Trim().Length > 40 ||
                !Regex.IsMatch("^[a-zA-Z\\s]+$", lastName.Trim()
                )) return CitizenErrors.LastNameNoValid;

            return null!;
        }

        public async Task<ValidationResult> ActiveCitizen(Guid Id, string Identification)
        {

            //agregar validaciones de elecciones activas del modulo de elecciones 

            var valid = await _citizensServiceValidate.ExistCitizensByIdentification(Identification);
            if (!valid) _errors.Add(CitizenErrors.NoExtisCitizen);

            var validActive = await _citizensServiceValidate.ExistOtherCitizensByState(Id, Identification, true);
            if (validActive) _errors.Add(CitizenErrors.ActiveNoValid);

            var stateValidOfModif = await _citizensServiceValidate.CurrentStateOfTheCitizen(Id);
            if (stateValidOfModif) _errors.Add(CitizenErrors.StateNoValidOfModifie);

            return _errors.Any() ? ValidationResult.Failure(_errors) : ValidationResult.Success();
        }

        public async Task<ValidationResult> CreateCitizen(Citizen citizen)
        {
            //agregar validaciones de elecciones activas del modulo de elecciones 

            if (citizen == null) _errors.Add(CitizenErrors.DataInvalid);
            var exitsCitizenByIdentification = await _citizensServiceValidate.ExistCitizensByIdentification(citizen!.IdentificationNumber.Value);
            var exitsCitizenByEmail = await _citizensServiceValidate.ExistCitizensByEmail(citizen.Email.Value);
            if(exitsCitizenByEmail || exitsCitizenByIdentification) _errors.Add(CitizenErrors.ExistCitizen);
            if (exitsCitizenByEmail) _errors.Add(CitizenErrors.ExistEmail);
            if (exitsCitizenByIdentification) _errors.Add(CitizenErrors.ExistIdentification);

            var validName = ValidateName(citizen.Name);
            if (validName != null) _errors.Add(CitizenErrors.NameNoValid);
            var validLastName = ValidateLastName(citizen.LastName);
            if (validLastName != null) _errors.Add(CitizenErrors.LastNameNoValid);
            if (!citizen.State) _errors.Add(CitizenErrors.StateNoValid);

            return _errors.Any() ? ValidationResult.Failure(_errors) : ValidationResult.Success();
        }

        public async Task<ValidationResult> DesactiveCitizen(Guid Id, string Identification)
        {
            var valid = await _citizensServiceValidate.ExistCitizensByIdentification(Identification);
            if (!valid) _errors.Add(CitizenErrors.NoExtisCitizen);

            var validActive = await _citizensServiceValidate.ExistOtherCitizensByState(Id, Identification, false);
            if (validActive) _errors.Add(CitizenErrors.DesactiveNoValid);

            var stateValidOfModif = await _citizensServiceValidate.CurrentStateOfTheCitizen(Id);
            if (stateValidOfModif) _errors.Add(CitizenErrors.StateNoValidOfModifie);

            return _errors.Any() ? ValidationResult.Failure(_errors) : ValidationResult.Success();
        }

        public async Task<ValidationResult> UpdateCitizen(Citizen citizen)
        {
            //agregar validacion de elecciones activas
            if (citizen == null) _errors.Add(CitizenErrors.DataInvalid);

            var exits = await _citizensServiceValidate.ExistCitizensByIdentification(citizen!.IdentificationNumber.Value);
            if (!exits) _errors.Add(CitizenErrors.NoExtisCitizen);

            var existOtherCitizen = await _citizensServiceValidate.ExistOtherCitizens(citizen.Id, citizen.IdentificationNumber.Value);
            if(existOtherCitizen) _errors.Add(CitizenErrors.ExistCitizen);

            var parcitedInEletions = await _citizensServiceValidate.CitizenParticipatedInElection(citizen.Id, citizen.IdentificationNumber.Value);
            if (parcitedInEletions) _errors.Add(CitizenErrors.ChangeIdentificationNoValid);

            var validateName = ValidateName(citizen.Name);
            if (validateName != null) _errors.Add(CitizenErrors.NameNoValid);

            var validateLastName = ValidateLastName(citizen.LastName);
            if (validateLastName != null) _errors.Add(CitizenErrors.LastNameNoValid);

            var exitsCitizenByEmail = await _citizensServiceValidate.ExistCitizensByEmail(citizen.Email.Value);
            if (exitsCitizenByEmail) _errors.Add(CitizenErrors.ExistEmail);


            return _errors.Any() ? ValidationResult.Failure(_errors) : ValidationResult.Success();
        }
    }
}
