using eVote360.Core.Domain.Contracts.DomainService.PoliticalParty;
using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Common.Errors;
using System.ComponentModel;

namespace eVote360.Core.Domain.Validators.PoliticalPartyValidator
{
    public class PoliticalPartyValidator : IPoliticalPartyValidator
    {
        private readonly IPoliticalPartyDomainService _service;

        public PoliticalPartyValidator(IPoliticalPartyDomainService service)
        {
            _service = service;
        }

        public async Task<ValidationResult> ValidateAlterState(int partyId)
        {
            var errors = new List<Error>();

            var exists = await _service.GetByIdEntitie(partyId);
            if (exists == null) {
                errors.Add(new Error("Partido Politico", "El partido politico no existe"));
                return ValidationResult.Failure(errors);
            }

          /*  var isElectionActive = await _electionService.ExistActiveElecion();
            if (isElectionActive)
            {
                errors.Add(PoliticalPartyError.CantDesactivateDuringElection);
                return ValidationResult.Failure(errors.ToArray());
            }
          */


            return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success();
        }

        public async Task<ValidationResult> ValidateCreate(Entities.PoliticalParty.PoliticalParty party)
        {
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(party.Name))
                errors.Add(PoliticalPartyError.PoliticalPartyNameIsRequired);

            else if (await _service.PoliticalPartyNameAlreadyExist(party.Name))
                errors.Add(PoliticalPartyError.PoliticalPartyNameAlreadyExist);

            if(party.PoliticalPartyAcronym == null || party.PoliticalPartyAcronym.Value.Length > 3)
                errors.Add(PoliticalPartyError.PoliticalPartyAcronymAlreadyExist);
            else if (await _service.ValidateUniqueAcronymAsync(party.PoliticalPartyAcronym.Value))
                    errors.Add(PoliticalPartyError.PoliticalPartyAcronymAlreadyExist);

            if (string.IsNullOrWhiteSpace(party.PoliticalPartyLogo))
                errors.Add(PoliticalPartyError.PoliticalPartyLogoIsRequired);

                return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success();
        }


        public async Task<ValidationResult> ValidateUpdate(Entities.PoliticalParty.PoliticalParty party)
        {
            var errors = new List<Error>();

            bool hasParticiped = await _service.PoliticalPartyAlreadyParticipated(party.Id);

            if (hasParticiped)
            {
                var currentParty = await _service.GetByIdEntitie(party.Id);

                if(currentParty.Name != party.Name || currentParty.PoliticalPartyAcronym.Value !=  party.PoliticalPartyAcronym.Value)
                    errors.Add(PoliticalPartyError.PoliticalPartyAlreadyParticipated);
            }

            return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success();
        }
        

    }
}
