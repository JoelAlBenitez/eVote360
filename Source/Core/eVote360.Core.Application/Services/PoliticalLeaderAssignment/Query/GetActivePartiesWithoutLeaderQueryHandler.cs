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
    public class GetActivePartiesWithoutLeaderQueryHandler : IGetActivePartiesWithoutLeaderQuery
    {
        private readonly IPoliticalLeaderAssignmentRepository _repository;

        public GetActivePartiesWithoutLeaderQueryHandler(IPoliticalLeaderAssignmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult<IEnumerable<PartyDropdownDto>>> ExecuteAsync()
        {
            var errors = new List<Error>();
            try
            {
                var data = await _repository.GetActivePartiesWithoutLeaderAsync();
                
                var dtos = data.Select(d => new PartyDropdownDto
                {
                    PartyId = d.PartyId,
                    Name = d.Name,
                    Siglas = d.Siglas
                });

                return ValidationResult<IEnumerable<PartyDropdownDto>>.Success(dtos);
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Error al consultar", ex.Message));
                return ValidationResult<IEnumerable<PartyDropdownDto>>.Failure(errors.ToArray());
            }
        }
    }
}
