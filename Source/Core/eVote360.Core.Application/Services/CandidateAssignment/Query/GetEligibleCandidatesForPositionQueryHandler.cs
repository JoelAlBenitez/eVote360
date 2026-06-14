using eVote360.Core.Application.Contracts.CandidateAssignment.Query;
using eVote360.Core.Application.DTOs.CandidateAssignment;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.CandidateAssignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Services.CandidateAssignment.QueryHandler
{
    public class GetEligibleCandidatesForPositionQueryHandler : IGetEligibleCandidatesForPositionQuery
    {
        private readonly ICandidateAssignmentRepository _repository;

        public GetEligibleCandidatesForPositionQueryHandler(ICandidateAssignmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult<IEnumerable<CandidateAssignmentDto>>> ExecuteAsync(int partyId, int electivePositionId)
        {
            var errors = new List<Error>();
            try
            {
                // El repositorio retorna IEnumerable<Candidates>
                var entities = await _repository.GetEligibleCandidatesAsync(partyId, electivePositionId);

                var dtos = entities.Select(entity => new CandidateAssignmentDto
                {
                    AssignmentId = null, // No hay asignación todavía
                    AssigningPartyId = partyId,
                    CandidateId = entity.Id,
                    ElectivePositionId = electivePositionId,
                    CandidateName = entity.Name?.Name,
                    CandidateLastName = entity.Name?.LastName,
                    PhotoUrl = entity.PhotoUrl?.PhotoUrl,
                    CandidateType = entity.PoliticalPartyId == partyId ? "Propio" : "Aliado"
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
