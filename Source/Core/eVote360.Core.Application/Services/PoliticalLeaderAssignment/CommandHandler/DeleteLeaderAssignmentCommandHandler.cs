using eVote360.Core.Application.Contracts.PoliticalLeaderAssignment.Commands;
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
    public class DeleteLeaderAssignmentCommandHandler : IDeleteLeaderAssignmentCommand
    {
        private readonly IPoliticalLeaderAssignmentRepository _repository;
        private readonly ILeaderAssignmentValidator _validator;

        public DeleteLeaderAssignmentCommandHandler(
            IPoliticalLeaderAssignmentRepository repository, 
            ILeaderAssignmentValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ValidationResult<bool>> ExecuteAsync(int assignmentId)
        {
            var errors = new List<Error>();
            try
            {
                var validation = await _validator.ValidateDeleteAsync(assignmentId);
                if (!validation.IsValid)
                {
                    return ValidationResult<bool>.Failure(validation.errors.ToList());
                }

                var deleted = await _repository.DeleteAsync(assignmentId);
                if (!deleted)
                {
                    errors.Add(new Error("Error al eliminar", "No se pudo eliminar la asignación del dirigente."));
                    return ValidationResult<bool>.Failure(errors.ToArray());
                }

                return ValidationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Error inesperado", ex.Message));
                return ValidationResult<bool>.Failure(errors.ToArray());
            }
        }
    }
}
