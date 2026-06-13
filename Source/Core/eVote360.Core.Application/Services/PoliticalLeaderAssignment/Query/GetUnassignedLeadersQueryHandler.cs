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
    public class GetUnassignedLeadersQueryHandler : IGetUnassignedLeadersQuery
    {
        private readonly IPoliticalLeaderAssignmentRepository _repository;

        public GetUnassignedLeadersQueryHandler(IPoliticalLeaderAssignmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult<IEnumerable<UserDropdownDto>>> ExecuteAsync()
        {
            var errors = new List<Error>();
            try
            {
                var data = await _repository.GetUnassignedLeadersAsync();
                
                var dtos = data.Select(d => new UserDropdownDto
                {
                    UserId = d.UserId,
                    FullName = d.FullName,
                    Username = d.Username
                });

                return ValidationResult<IEnumerable<UserDropdownDto>>.Success(dtos);
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Error al consultar", ex.Message));
                return ValidationResult<IEnumerable<UserDropdownDto>>.Failure(errors.ToArray());
            }
        }
    }
}
