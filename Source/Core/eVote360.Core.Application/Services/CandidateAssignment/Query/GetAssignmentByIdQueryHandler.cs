using eVote360.Core.Application.CandidateAssignment.DTOs;
using eVote360.Core.Application.Contracts.CandidateAssignment.Query;
using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.CandidateAssignment;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Services.CandidateAssignment.QueryHandler
{
    public class GetAssignmentByIdQueryHandler : IGetAssignmentByIdQuery
    {
        private readonly ICandidateAssignmentRepository _repository;

        public GetAssignmentByIdQueryHandler(ICandidateAssignmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult<CandidateAssignmentDto>> ExecuteAsync(int assignmentId)
        {
            var errors = new List<Error>();
            try
            {
                var entity = await _repository.GetByIdEntitie(assignmentId);
                if (entity == null)
                {
                    errors.Add(AssignmentErrors.AssignmentNotFound);
                    return ValidationResult<CandidateAssignmentDto>.Failure(errors.ToArray());
                }

                var dto = new CandidateAssignmentDto
                {
                    AssignmentId = entity.Id,
                    AssigningPartyId = entity.AssigningPartyId,
                    CandidateId = entity.CandidateId,
                    ElectivePositionId = entity.ElectivePositionId
                    // Los campos de nombre, foto, tipo se llenan en Infrastructure con joinsss x3
                };

                return ValidationResult<CandidateAssignmentDto>.Success(dto);
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Error al consultar", ex.Message));
                return ValidationResult<CandidateAssignmentDto>.Failure(errors.ToArray());
            }
        }
    }
}
