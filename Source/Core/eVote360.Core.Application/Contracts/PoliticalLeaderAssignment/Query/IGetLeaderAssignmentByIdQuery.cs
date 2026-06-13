using eVote360.Core.Application.DTOs.PoliticalLeaderAssignment;
using eVote360.Core.Domain.Common.ValidationResult;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Contracts.PoliticalLeaderAssignment.Query
{
    public interface IGetLeaderAssignmentByIdQuery
    {
        Task<ValidationResult<LeaderAssignmentDto>> ExecuteAsync(int assignmentId);
    }
}
