using eVote360.Core.Application.Contracts.Election.Commands;
using eVote360.Core.Application.DTOs.Election;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.ElectionRepository;
using System.Xml;
using ElectionEntity = eVote360.Core.Domain.Entities.Election.Election;
using ElectionEnum = eVote360.Core.Domain.Common.Enums.ElectionState;
using ElectionDate = eVote360.Core.Domain.Settings.ValueObjects.ElectionDate.ElectionDate;
using eVote360.Core.Domain.Validators.ElectionValidator;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Application.Contracts.Authentication.Command;

namespace eVote360.Core.Application.Services.Election.CommandHandler
{
    public sealed class ElectionCreate : IElectionCreateCommand
    {
        private readonly IElectionRepository _repository;
        private readonly IElectionValidator _validator;
        private readonly ISessionUser _sessionUser;

        public ElectionCreate(IElectionRepository repository, IElectionValidator validator, ISessionUser sessionUser)
        {
            _repository = repository;
            _validator = validator;
            _sessionUser = sessionUser;
        }

        public async Task<ValidationResult> ExecuteAsync(ElectionDto dto)
        {
            var errors = new List<Error>();

            try
            {

                var election = new ElectionEntity
                {
                    Id = 0,
                    CreateAt = DateTime.UtcNow,
                    CreateUserId = _sessionUser.GetUserId(),
                    State = true,

                    Name = dto.Name,
                    ElectionDate = new ElectionDate(dto.ElectionDate),

                    ElectionState = ElectionEnum.Pendiente
                };

                var result = await _validator.ValidateElection(election);

                if (!result.IsValid)
                {
                    return result;
                }
                var isCreated = await _repository.CreateEntiteAsync(election);
                if (!isCreated)
                {
                    errors.Add(new Error("ELEC CREATE FAIL", "No se pudo crear la eleccion"));
                    return ValidationResult.Failure(errors);
                }

                return ValidationResult.Success();
            }

            catch (ArgumentException ex)
            {
                errors.Add(new Error("ELEC VALIDATION", ex.Message));
                return ValidationResult.Failure(errors);
            }
        }
    }
}
