using eVote360.Core.Application.DTOs.CandidateAssignment;
using eVote360.Core.Domain.Common.ValidationResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Contracts.CandidateAssignment.Query
{
    public interface IGetEligibleCandidatesForPositionQuery
    {
        Task<ValidationResult<IEnumerable<CandidateAssignmentDto>>> ExecuteAsync(int partyId, int electivePositionId);
    }
}
