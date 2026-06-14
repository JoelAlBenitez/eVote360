using eVote360.Core.Application.Contracts.CandidateAssignment.Commands;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.CandidateAssignment;
using eVote360.Core.Domain.Contracts.Validators.CandidateAssignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Services.CandidateAssignment.CommandHandler
{
    public class DeleteAssignmentCommandHandler : IDeleteAssignmentCommand
    {
        private readonly ICandidateAssignmentRepository _repository;
        private readonly IAssignmentValidator _validator;

        public DeleteAssignmentCommandHandler(ICandidateAssignmentRepository repository, IAssignmentValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ValidationResult<bool>> ExecuteAsync(int assignmentId, int assigningPartyId)
        {
            var errors = new List<Error>();
            try
            {
                var validation = await _validator.ValidateDeleteAsync(assignmentId, assigningPartyId);
                if (!validation.IsValid)
                {
                    return ValidationResult<bool>.Failure(validation.errors.ToList());
                }

                var deleted = await _repository.DeleteAsync(assignmentId);
                if (!deleted)
                {
                    errors.Add(new Error("Error al eliminar", "No se pudo eliminar la asignación del candidato."));
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
