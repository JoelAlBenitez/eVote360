using eVote360.Core.Application.Contracts.Authentication.Command;
using eVote360.Core.Application.Contracts.PoliticalParty.Commands;
using eVote360.Core.Application.DTOs.PoliticalParty;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalParty;
using eVote360.Core.Domain.Settings.ValueObjects.PoliticalPartyAcronym;
using eVote360.Core.Domain.Validators.PoliticalPartyValidator;
using Error = eVote360.Core.Domain.Common.Errors.Error;
using PartyEntity = eVote360.Core.Domain.Entities.PoliticalParty.PoliticalParty;
using PhotoValidator = eVote360.Core.Domain.Settings.ValueObjects.Candidate.CandidatePhoto;

namespace eVote360.Core.Application.Services.PoliticalParty.CommandHandler
{
    public sealed class PoliticalPartyUpdate : IPoliticalPartyUpdateCommand
    {
        private readonly IPoliticalPartyRepository _repository;
        private readonly IPoliticalPartyValidator _validator;
        private readonly ISessionUser _sessionUser;


        public PoliticalPartyUpdate(IPoliticalPartyRepository repository, IPoliticalPartyValidator validator, ISessionUser sessionUser)
        {
            _repository = repository;
            _validator = validator;
            _sessionUser = sessionUser;
        }

        public async Task<ValidationResult> ExecuteAsync(PoliticalPartyDto dto)
        {
            var errors = new List<Error>();
            try
            {
                if (dto.Id == null || dto.Id <= 0)
                {
                    errors.Add(new Error("PARTY ID INVALID", "El Id del partido no es valido para edicion"));
                    return ValidationResult.Failure(errors);
                }

                var party = new PartyEntity
                {
                    Id = dto.Id,
                    CreateAt = dto.CreateAt,
                    CreateUserId = dto.CreateUserId,

                    Name = dto.Name!,
                    PoliticalPartyDescription = dto.PoliticalPartyDescription,
                    PoliticalPartyLogo = new PhotoValidator(dto.PoliticalPartyLogo),
                    State = dto.State,

                    UpdateAt = DateTime.UtcNow,
                    UpdateUserId = _sessionUser.GetUserId(),

                    PoliticalPartyAcronym = new PoliticalPartyAcronym(dto.PoliticalPartyAcronym)
                };
                var result = await _validator.ValidateUpdate(party);

                if (!result.IsValid)
                    return result;

                var isUpdated = await _repository.UpdateEntitieAsync(party);

                if (!isUpdated)
                {
                    errors.Add(new Error("UPD FAIL","No se puedo actualizar el Partido."));
                    return ValidationResult.Failure(errors);
                }
                return ValidationResult.Success();
            }
            catch (ArgumentException ex) {
                errors.Add(new Error("PARTY VALIDATION ERROR", ex.Message));
                return ValidationResult.Failure(errors);
            }
        }
    }
}
