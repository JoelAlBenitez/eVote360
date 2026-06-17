using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.ServiceValidates.Citizens;
using eVote360.Core.Domain.Contracts.ServiceValidates.Election;
using eVote360.Core.Domain.Contracts.ServiceValidates.Elector.Votes;
using eVote360.Core.Domain.Entities.Citizens;
using System.Text.RegularExpressions;

namespace eVote360.Core.Domain.Validators.CitizensValidator
{
    public class CitizensValidator : ICitizensValidator
    {

        private readonly ICitizensServiceValidate _citizensServiceValidate;
        private readonly IElectionDomainService _electionDomainService;
        private readonly IVotesValidate _votesValidate;
        private List<Error> _errors = new List<Error>();

        public CitizensValidator(ICitizensServiceValidate citizensServiceValidate,
            IElectionDomainService electionDomainService,
            IVotesValidate votesValidate
            
            )
        {
            _citizensServiceValidate = citizensServiceValidate;
            _electionDomainService = electionDomainService;
            _votesValidate = votesValidate;
        }
        private Error ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)
                || name.Trim().Length > 40 ||
                !Regex.IsMatch(name.Trim(), @"^[\p{L}\s]+$"
                )) return  CitizenErrors.NameNoValid;

            return null!;
        }

        private Error ValidateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName)
                || lastName.Trim().Length > 40 ||
                !Regex.IsMatch(lastName.Trim(), @"^[\p{L}\s]+$"
                )) return CitizenErrors.LastNameNoValid;

            return null!;
        }

        public async Task<ValidationResult> ActiveCitizen(Guid Id, string Identification)
        {
            var errors = new List<Error>();
            var existElectionActive = await _electionDomainService.ExistActiveElection();
            if (existElectionActive)
            {
                errors.Add(ElectionError.ElectionActive);
                return ValidationResult.Failure(errors);
            }

            var valid = await _citizensServiceValidate.ExistCitizensByIdentification(Identification);
            if (!valid) errors.Add(CitizenErrors.NoExtisCitizen);

            var validActive = await _citizensServiceValidate.ExistOtherCitizensByState(Id, Identification, true);
            if (validActive) errors.Add(CitizenErrors.ActiveNoValid);

            var stateValidOfModif = await _citizensServiceValidate.CurrentStateOfTheCitizen(Id);
            if (stateValidOfModif) errors.Add(CitizenErrors.StateNoValidOfModifie);

            return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success();
        }

        public async Task<ValidationResult> CreateCitizen(Citizen citizen)
        {
            var errors = new List<Error>();
            var existElectionActive = await _electionDomainService.ExistActiveElection();
            if (existElectionActive)
            {
                errors.Add(ElectionError.ElectionActive);
                return ValidationResult.Failure(errors);
            }

            if (citizen == null)
            {
                errors.Add(CitizenErrors.DataInvalid);
                return ValidationResult.Failure(errors);
            }

            var exitsCitizenByIdentification = await _citizensServiceValidate.ExistCitizensByIdentification(citizen!.IdentificationNumber.Value);
            var exitsCitizenByEmail = await _citizensServiceValidate.ExistCitizensByEmail(citizen.Email.Value, null);
            if(exitsCitizenByEmail || exitsCitizenByIdentification) errors.Add(CitizenErrors.ExistCitizen);
            if (exitsCitizenByEmail) errors.Add(CitizenErrors.ExistEmail);
            if (exitsCitizenByIdentification) errors.Add(CitizenErrors.ExistIdentification);

            var validName = ValidateName(citizen.Name);
            if (validName != null) errors.Add(CitizenErrors.NameNoValid);
            var validLastName = ValidateLastName(citizen.LastName);
            if (validLastName != null) errors.Add(CitizenErrors.LastNameNoValid);

            return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success();
        }

        public async Task<ValidationResult> DesactiveCitizen(Guid Id, string Identification)
        {
            var errors = new List<Error>();
            var existElectionActive = await _electionDomainService.ExistActiveElection();
            if (existElectionActive)
            {
                errors.Add(ElectionError.ElectionActive);
                return ValidationResult.Failure(errors);
            }

            var valid = await _citizensServiceValidate.ExistCitizensByIdentification(Identification);
            if (!valid) errors.Add(CitizenErrors.NoExtisCitizen);

            var validActive = await _citizensServiceValidate.ExistOtherCitizensByState(Id, Identification, false);
            if (validActive) errors.Add(CitizenErrors.DesactiveNoValid);

            var stateValidOfModif = await _citizensServiceValidate.CurrentStateOfTheCitizen(Id);
            if (!stateValidOfModif) errors.Add(CitizenErrors.StateNoValidOfModifie);

            return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success();
        }

        public async Task<ValidationResult> UpdateCitizen(Citizen citizen)
        {
            var errors = new List<Error>();
            var existElectionActive = await _electionDomainService.ExistActiveElection();
            if (existElectionActive)
            {
                errors.Add(ElectionError.ElectionActive);
                return ValidationResult.Failure(errors);
            }

            if (citizen == null)
            {
                errors.Add(CitizenErrors.DataInvalid);
                return ValidationResult.Failure(errors);
            }

            var exits = await _citizensServiceValidate.ExistCitizensByIdentification(citizen!.IdentificationNumber.Value);
            if (!exits) errors.Add(CitizenErrors.NoExtisCitizen);

            var existOtherCitizen = await _citizensServiceValidate.ExistOtherCitizens(citizen.Id, citizen.IdentificationNumber.Value);
            if(existOtherCitizen) errors.Add(CitizenErrors.ExistCitizen);

            var parcitedInEletions = await _votesValidate.CitizenParticipatedInElection(citizen.Id, citizen.IdentificationNumber.Value);
            if (parcitedInEletions) errors.Add(CitizenErrors.ChangeIdentificationNoValid);

            var validateName = ValidateName(citizen.Name);
            if (validateName != null) errors.Add(CitizenErrors.NameNoValid);

            var validateLastName = ValidateLastName(citizen.LastName);
            if (validateLastName != null) errors.Add(CitizenErrors.LastNameNoValid);

            var exitsCitizenByEmail = await _citizensServiceValidate.ExistCitizensByEmail(citizen.Email.Value, citizen.Id);
            if (exitsCitizenByEmail) errors.Add(CitizenErrors.ExistEmail);


            return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success();
        }
    }
}
