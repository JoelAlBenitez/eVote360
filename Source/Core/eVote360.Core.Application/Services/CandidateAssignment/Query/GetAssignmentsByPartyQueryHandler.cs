using eVote360.Core.Application.CandidateAssignment.DTOs;
using eVote360.Core.Application.Contracts.CandidateAssignment.Query;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.CandidateAssignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Services.CandidateAssignment.QueryHandler
{
    public class GetAssignmentsByPartyQueryHandler : IGetAssignmentsByPartyQuery
    {
        private readonly ICandidateAssignmentRepository _repository;

        public GetAssignmentsByPartyQueryHandler(ICandidateAssignmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult<IEnumerable<CandidateAssignmentDto>>> ExecuteAsync(int partyId)
        {
            var errors = new List<Error>();
            try
            {
                var entities = await _repository.GetAllByPartyIdAsync(partyId);
                
                var dtos = entities.Select(entity => new CandidateAssignmentDto
                {
                    AssignmentId = entity.Id,
                    AssigningPartyId = entity.AssigningPartyId,
                    CandidateId = entity.CandidateId,
                    ElectivePositionId = entity.ElectivePositionId
                    // Los campos de nombre, foto, tipo se llenan en Infrastructure con joinss x2
                });

                return ValidationResult<IEnumerable<CandidateAssignmentDto>>.Success(dtos);
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Error al consultar", ex.Message));
                return ValidationResult<IEnumerable<CandidateAssignmentDto>>.Failure(errors.ToArray());
            }
        }
    }
}
