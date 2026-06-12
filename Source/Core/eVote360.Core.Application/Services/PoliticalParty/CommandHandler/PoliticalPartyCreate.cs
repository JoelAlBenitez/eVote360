using eVote360.Core.Application.Contracts.PoliticalParty.Commands;
using eVote360.Core.Application.DTOs.PoliticalParty;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalParty;
using eVote360.Core.Domain.Validators.PoliticalPartyValidator;
using eVote360.Core.Domain.Settings.ValueObjects.PoliticalPartyAcronym;
using PartyEntity = eVote360.Core.Domain.Entities.PoliticalParty.PoliticalParty;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Common.Errors;

namespace eVote360.Core.Application.Services.PoliticalParty.CommandHandler
{
    public sealed class PoliticalPartyCreate : IPoliticalPartyCreateCommand
    {
        private readonly IPoliticalPartyRepository _repository;
        private readonly IPoliticalPartyValidator _validator;



        public PoliticalPartyCreate(IPoliticalPartyRepository repository, IPoliticalPartyValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ValidationResult> ExecuteAsync(PoliticalPartyDto dto)
        {
            var errors = new List<Error>();

            try
            {
                var party = new PartyEntity
                {
                    Id = 0,
                    CreateAt = DateTime.UtcNow,
                    CreateUserId = 1,

                    Name = dto.Name!,
                    PoliticalPartyDescription = dto.PoliticalPartyDescription,
                    PoliticalPartyLogo = dto.PoliticalPartyLogo,
                    State = dto.State,

                    PoliticalPartyAcronym = new PoliticalPartyAcronym(dto.PoliticalPartyAcronym)
                };
                var result = await _validator.ValidateCreate(party);

                if (!result.IsValid)
                    return result;

                var isCreated = await _repository.CreateEntiteAsync(party);

                if (!isCreated)
                {
                    errors.Add(new Error("CREATE FAIL", "Error al guardar el partido politico"));
                    return ValidationResult.Failure(errors);
                }
                return ValidationResult.Success();
            }

            catch (ArgumentException ex)
            {
                errors.Add(new Error("ERROR DOMAIN PARTIDO", ex.Message));

                return ValidationResult.Failure(errors);
            }
        }
    }
}
