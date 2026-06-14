using eVote360.Core.Application.Contracts.PoliticalLeaderAssignment.Commands;
using eVote360.Core.Application.DTOs.PoliticalLeaderAssignment;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalAssignment;
using eVote360.Core.Domain.Validators.PoliticalAssignment;
using AssignmentEntity = eVote360.Core.Domain.Entities.PoliticalAssignment.PoliticalAssignment;


namespace eVote360.Core.Application.Services.PoliticalLeaderAssignment.CommandHandler
{
    public sealed class LeaderAssignmentCreate : ILeaderAssignmentCreateCommand
    {
        IPoliticalAssignmentRepository _repository;
        IPoliticalAssignmentValidator _validator;

    public LeaderAssignmentCreate(IPoliticalAssignmentRepository repository, IPoliticalAssignmentValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ValidationResult> ExecuteAsync(LeaderAssignmentDto dto)
        {
            var errors = new List<Error>();

            try { 
            var assignment = new AssignmentEntity
            {
                Id = 0,
                CreateAt = DateTime.UtcNow,
                CreateUserId = dto.CreateUserId,

                Name = dto.Name ?? "Asignacion Creada",

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

                var isCreated = await _repository.CreateEntiteAsync(assignment);

                if (!isCreated)
                {
                    errors.Add(new Error("ASSIGN CREATE FAIL", "No se pudo crear la asignacion de lider"));
                    return ValidationResult.Failure(errors);
                }

                return ValidationResult.Success();
            }
            catch (ArgumentException ex)
            {
                errors.Add(new Error("ASSIGN ERROR", ex.Message));
                return ValidationResult.Failure(errors);

            }
        }
    }
}
