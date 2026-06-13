using eVote360.Core.Application.Contracts.PoliticalLeaderAssignment.Commands;
using eVote360.Core.Application.DTOs.PoliticalLeaderAssignment;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalLeaderAssignment;
using eVote360.Core.Domain.Contracts.Validators.PoliticalLeaderAssignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Services.PoliticalLeaderAssignment.CommandHandler
{
    public class CreateLeaderAssignmentCommandHandler : ICreateLeaderAssignmentCommand
    {
        private readonly IPoliticalLeaderAssignmentRepository _repository;
        private readonly ILeaderAssignmentValidator _validator;

        public CreateLeaderAssignmentCommandHandler(
            IPoliticalLeaderAssignmentRepository repository, 
            ILeaderAssignmentValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ValidationResult<LeaderAssignmentDto>> ExecuteAsync(CreateLeaderAssignmentRequestDto request, int authenticatedUserId)
        {
            var errors = new List<Error>();
            try
            {
                var validation = await _validator.ValidateCreateAsync(request.UserId, request.PoliticalPartyId);
                if (!validation.IsValid)
                {
                    return ValidationResult<LeaderAssignmentDto>.Failure(validation.errors.ToList());
                }

                var entity = new Core.Domain.Entities.PoliticalLeaderAssignment.PoliticalLeaderAssignment
                {
                    UserId = request.UserId,
                    PoliticalPartyId = request.PoliticalPartyId,
                    CreateAt = DateTimeOffset.UtcNow,
                    CreateUserId = authenticatedUserId
                };

                var created = await _repository.CreateEntiteAsync(entity);
                if (!created)
                {
                    errors.Add(new Error("Error en la creación", "No se pudo crear la asignación del dirigente."));
                    return ValidationResult<LeaderAssignmentDto>.Failure(errors.ToArray());
                }

                var dto = new LeaderAssignmentDto
                {
                    Id = entity.Id,
                    UserId = entity.UserId,
                    PoliticalPartyId = entity.PoliticalPartyId
                    // Los campos descriptivos (nombres, siglas) se llenarán en UI o via query específica
                };

                return ValidationResult<LeaderAssignmentDto>.Success(dto);
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Error inesperado", ex.Message));
                return ValidationResult<LeaderAssignmentDto>.Failure(errors.ToArray());
            }
        }
    }
}
