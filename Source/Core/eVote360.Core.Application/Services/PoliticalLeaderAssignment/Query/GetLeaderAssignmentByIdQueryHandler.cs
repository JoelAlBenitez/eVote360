using eVote360.Core.Application.Contracts.PoliticalLeaderAssignment.Query;
using eVote360.Core.Application.DTOs.PoliticalLeaderAssignment;
using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalLeaderAssignment;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Services.PoliticalLeaderAssignment.QueryHandler
{
    public class GetLeaderAssignmentByIdQueryHandler : IGetLeaderAssignmentByIdQuery
    {
        private readonly IPoliticalLeaderAssignmentRepository _repository;

        public GetLeaderAssignmentByIdQueryHandler(IPoliticalLeaderAssignmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult<LeaderAssignmentDto>> ExecuteAsync(int assignmentId)
        {
            var errors = new List<Error>();
            try
            {
                var entity = await _repository.GetByIdEntitie(assignmentId);
                
                if (entity == null)
                {
                    errors.Add(LeaderAssignmentErrors.AssignmentNotFound);
                    return ValidationResult<LeaderAssignmentDto>.Failure(errors.ToArray());
                }

                var dto = new LeaderAssignmentDto
                {
                    Id = entity.Id,
                    UserId = entity.UserId,
                    PoliticalPartyId = entity.PoliticalPartyId
                    // Nombres y siglas se llenarán en la infraestructura o en otro lugar
                };

                return ValidationResult<LeaderAssignmentDto>.Success(dto);
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Error al consultar", ex.Message));
                return ValidationResult<LeaderAssignmentDto>.Failure(errors.ToArray());
            }
        }
    }
}
