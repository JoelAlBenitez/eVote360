using eVote360.Core.Application.DTOs.PoliticalLeaderAssignment;
using eVote360.Core.Domain.Common.ValidationResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Contracts.PoliticalLeaderAssignment.Query
{
    public interface IGetUnassignedLeadersQuery
    {
        Task<ValidationResult<IEnumerable<UserDropdownDto>>> ExecuteAsync();
    }
}
