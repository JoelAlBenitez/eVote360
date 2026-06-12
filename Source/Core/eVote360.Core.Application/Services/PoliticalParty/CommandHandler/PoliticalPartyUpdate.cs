using eVote360.Core.Application.Contracts.PoliticalParty.Commands;
using eVote360.Core.Application.DTOs.PoliticalParty;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalParty;
using eVote360.Core.Domain.Validators.PoliticalPartyValidator;
using eVote360.Core.Domain.ValueObjects;

using PartyEntity = eVote360.Core.Domain.Entities.PoliticalParty.PoliticalParty;
using Error = eVote360.Core.Domain.Common.Errors.Error;
using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Services.PoliticalParty.CommandHandler
{
    public sealed class PoliticalPartyUpdate : IPoliticalPartyUpdateCommand
    {
        private readonly IPoliticalPartyRepository _repository;
        private readonly IPoliticalPartyValidator _validator;



        public PoliticalPartyUpdate(IPoliticalPartyRepository repository, IPoliticalPartyValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ValidationResult> ExecuteAsync(PoliticalPartyDto dto)
        {
            var party = new PartyEntity
            {
                Id = dto.Id,
                CreateAt = dto.CreateAt,
                CreateUserId = 1,

                Name = dto.Name!,
                PoliticalPartyDescription = dto.PoliticalPartyDescription,
                PoliticalPartyLogo = dto.PoliticalPartyLogo,
                State = dto.State,

                UpdateAt = DateTime.UtcNow,
                UpdateUserId = dto.UpdateUserId,

                PoliticalPartyAcronym = new PoliticalPartyAcronym(dto.PoliticalPartyAcronym)
            };
            var result = await _validator.ValidateUpdate(party);

            if (!result.IsValid)
                return result;

            await _repository.UpdateEntitieAsync(party);

            return ValidationResult.Success();
        }
    }
}
