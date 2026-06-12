using eVote360.Core.Application.Contracts.PoliticalLeaderAssignment.Commands;
using eVote360.Core.Application.DTOs.PoliticalLeaderAssignment;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalAssignment;
using eVote360.Core.Domain.Validators.PoliticalAssignment;
using AssignmentEntity = eVote360.Core.Domain.Entities.PoliticalAssignment.PoliticalAssignment;



namespace eVote360.Core.Application.Services.PoliticalLeaderAssignment.CommandHandler
{
    public sealed class LeaderAssignmentUpdate : ILeaderAssignmentUpdateCommand
    {
       private readonly IPoliticalAssignmentRepository _repository;
       private readonly IPoliticalAssignmentValidator _validator;

        public LeaderAssignmentUpdate(IPoliticalAssignmentRepository repository, IPoliticalAssignmentValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ValidationResult> ExecuteAsync(LeaderAssignmentDto dto)
        {
            if (dto.Id <= 0)
            {
                var errors = new List<Error> { new Error(" ASIG. ID", "El ID es invalido") };
                return ValidationResult.Failure(errors);
            }

            var assignment = new AssignmentEntity
            {
                Id = dto.Id,
                CreateAt = dto.CreateAt,
                CreateUserId = dto.CreateUserId,

                UpdateAt = DateTime.UtcNow,
                UpdateUserId = dto.UpdateUserId,

                Name = dto.Name ?? "Asignacion Actualizada",

                PoliticalLeaderId = dto.PoliticalLeaderId,
                PoliticalPartyId = dto.PoliticalPartyId,
                State = dto.State,
                PolitcalAssignmentDate = dto.PoliticalAssignmentDate
            };

            var validationResult = await _validator.ValidatePoliticalAssignment(assignment);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            await _repository.UpdateEntitieAsync(assignment);

            return ValidationResult.Success();

        }
    }
}
