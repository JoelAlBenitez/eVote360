using eVote360.Core.Application.Contracts.PoliticalLeaderAssignment.Query;
using eVote360.Core.Application.DTOs.PoliticalLeaderAssignment;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalLeaderAssignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Services.PoliticalLeaderAssignment.QueryHandler
{
    public class GetAllLeaderAssignmentsQueryHandler : IGetAllLeaderAssignmentsQuery
    {
        private readonly IPoliticalLeaderAssignmentRepository _repository;

        public GetAllLeaderAssignmentsQueryHandler(IPoliticalLeaderAssignmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult<IEnumerable<LeaderAssignmentDto>>> ExecuteAsync()
        {
            var errors = new List<Error>();
            try
            {
                var entities = await _repository.GetAllAsync();

                var dtos = entities.Select(e => new LeaderAssignmentDto
                {
                    Id = e.Id,
                    UserId = e.UserId,
                    PoliticalPartyId = e.PoliticalPartyId
                    // Nombres y siglas se llenarán en la infraestructura o en otro lugar
                });

                return ValidationResult<IEnumerable<LeaderAssignmentDto>>.Success(dtos);
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Error al consultar", ex.Message));
                return ValidationResult<IEnumerable<LeaderAssignmentDto>>.Failure(errors.ToArray());
            }
        }
    }
}
