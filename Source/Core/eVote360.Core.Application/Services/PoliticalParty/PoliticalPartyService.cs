using eVote360.Core.Application.Contracts.PoliticalParty;
using eVote360.Core.Application.DTOs.PoliticalParty;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalParty;
using eVote360.Core.Domain.Validators.PoliticalPartyValidator;
using eVote360.Core.Domain.ValueObjects;
using PartyEntity = eVote360.Core.Domain.Entities.PoliticalParty.PoliticalParty;
using Error = eVote360.Core.Domain.Common.Errors.Error;

namespace eVote360.Core.Application.Services.PoliticalParty
{
    public  class PoliticalPartyService : IPoliticalPartyService
    {
        private readonly IPoliticalPartyRepository _repository;
        private readonly IPoliticalPartyValidator _validator;



        public PoliticalPartyService(IPoliticalPartyRepository repository, IPoliticalPartyValidator validator) 
        {
            _repository = repository;
            _validator = validator;
        }


        public async Task<ValidationResult> CreateAsync(PoliticalPartyDto dto)
        {
            var party = new PartyEntity
            {
                Id = 0,
                CreateAt = DateTime.UtcNow,
                CreateUserId = 1,

                Name = dto.Name!,
                PoliticalPartyDescription = dto.PoliticalPartyDescription,
                PoliticalPartyLogo = dto.PoliticalPartyLogo,
                PoliticalPartyState = dto.State,

                PoliticalPartyAcronym = new PoliticalPartyAcronym(dto.PoliticalPartyAcronym)
            };
            var result = await _validator.ValidateCreate(party);

            if(!result.IsValid)
                return result;
            
            await _repository.CreateEntiteAsync(party);

            return ValidationResult.Success();
        }

        public async Task<ValidationResult> UpdateAsync(PoliticalPartyDto dto)
        {
            var party = new PartyEntity
            {
                Id = dto.Id,
                CreateAt = dto.CreateAt,
                CreateUserId = dto.CreateUserId,
                UpdateAt = DateTime.UtcNow,
                UpdateUserId = dto.UpdateUserId,

                Name = dto.Name!,
                PoliticalPartyDescription = dto.PoliticalPartyDescription,
                PoliticalPartyLogo = dto.PoliticalPartyLogo,
                PoliticalPartyState = dto.State,

                PoliticalPartyAcronym = new PoliticalPartyAcronym(dto.PoliticalPartyAcronym)
            };

            var result = await _validator.ValidateUpdate(party);

            if(!result.IsValid)
                return result;

            await _repository.UpdateEntitieAsync(party);

            return ValidationResult.Success();
        }
        public async Task<IEnumerable<PoliticalPartyDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();

            return entities.Select(p => new PoliticalPartyDto
            {
                Name = p.Name,
                State = p.PoliticalPartyState,
                PoliticalPartyDescription= p.PoliticalPartyDescription,
                PoliticalPartyAcronym = p.PoliticalPartyAcronym.Value,
                PoliticalPartyLogo= p.PoliticalPartyLogo,
                CreateAt = p.CreateAt,
                CreateUserId = p.CreateUserId,
            });
        }

        public async Task<IEnumerable<PoliticalPartyDto>> GetActivePartiesAsync()
        {
            var entities = await _repository.GetActivePartiesAsync();

            return entities.Select(p => new PoliticalPartyDto
            {
                Name = p.Name,
                State = p.PoliticalPartyState,
                PoliticalPartyDescription = p.PoliticalPartyDescription,
                PoliticalPartyAcronym = p.PoliticalPartyAcronym.Value,
                PoliticalPartyLogo = p.PoliticalPartyLogo,
                CreateAt = p.CreateAt,
                CreateUserId = p.CreateUserId,
            }).ToList();

        }

        public async Task<PoliticalPartyDto> GetByIdAsync(int id)
        {
            var p = await _repository.GetByIdEntitie(id);

            if (p == null)
                return null!;

            return new PoliticalPartyDto
            {
                Name = p.Name,
                State = p.PoliticalPartyState,
                PoliticalPartyDescription = p.PoliticalPartyDescription,
                PoliticalPartyAcronym = p.PoliticalPartyAcronym.Value,
                PoliticalPartyLogo = p.PoliticalPartyLogo,
                CreateAt = p.CreateAt,
                CreateUserId = p.CreateUserId,
            };
        }

        public async Task<ValidationResult> AlterStateAsync(int id)
        {
            var validationResult = await _validator.ValidateAlterState(id);

            if(!validationResult.IsValid)
                return validationResult;

            var result = await _repository.AlterPartyStateAsync(id);

            if (!result)
                return ValidationResult.Failure(new Error("PP.AlterState", "No se puede cambiar el estado del partido politico"));

            return ValidationResult.Success();
        }
    }
}
