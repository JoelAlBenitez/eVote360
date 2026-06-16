using eVote360.Core.Application.Contracts.PoliticalParty.Commands;
using eVote360.Core.Application.DTOs.PoliticalParty;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalParty;
using eVote360.Core.Domain.Validators.PoliticalPartyValidator;
using eVote360.Core.Domain.Settings.ValueObjects.PoliticalPartyAcronym;

using PartyEntity = eVote360.Core.Domain.Entities.PoliticalParty.PoliticalParty;
using Error = eVote360.Core.Domain.Common.Errors.Error;
using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Services.PoliticalParty.CommandHandler
{
    public sealed class PoliticalPartyState : IPoliticalPartyStateCommand
    {
        private readonly IPoliticalPartyRepository _repository;
        private readonly IPoliticalPartyValidator _validator;



        public PoliticalPartyState(IPoliticalPartyRepository repository, IPoliticalPartyValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ValidationResult> ExecuteAsync(int id)
        {
            var validationResult = await _validator.ValidateAlterState(id);

            if (!validationResult.IsValid)
                return validationResult;

            var result = await _repository.AlterPartyStateAsync(id);

            if (!result)
                return ValidationResult.Failure(new List<Error> { new Error("PP.AlterState", "No se puede cambiar el estado del partido politico") });

            return ValidationResult.Success();
        }
    }
}
