using eVote360.Core.Application.Contracts.Election.Commands;
using eVote360.Core.Application.DTOs.Election;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.ElectionRepository;
using System.Xml;
using ElectionEntity = eVote360.Core.Domain.Entities.Election.Election;
using ElectionEnum = eVote360.Core.Domain.Common.Enums.ElectionState;
using ElectionDate = eVote360.Core.Domain.ValueObjects.ElectionDate;
using eVote360.Core.Domain.Validators.ElectionValidator;

namespace eVote360.Core.Application.Services.Election.CommandHandler
{
    public sealed class ElectionUpdate : IElectionUpdateCommand
    {
        private readonly IElectionRepository _repository;
        private readonly IElectionValidator _validator;

        public ElectionUpdate(IElectionRepository repository, IElectionValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ValidationResult> ExecuteAsync(ElectionDto dto)
        {
            var election = new ElectionEntity
            {
                Id = dto.Id,
                CreateAt = dto.CreateAt,
                CreateUserId = dto.CreateUserId,
                State = dto.State,
                UpdateAt = DateTime.UtcNow,
                UpdateUserId = dto.UpdateUserId,

                Name = dto.Name,
                ElectionDate = new ElectionDate(dto.ElectionDate),

                ElectionState = dto.ElectionState
            };

            var result = await _validator.ValidateElection(election);

            if (!result.IsValid)
            {
                return result;
            }
            await _repository.UpdateEntitieAsync(election);

            return ValidationResult.Success();
        }
    }
}
