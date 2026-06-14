using eVote360.Core.Application.DTOs.PoliticalLeaderAssignment;
using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Contracts.PoliticalLeaderAssignment.Query
{
    public interface  ILeaderAssignmentGetByIdQuery
    {
        Task<ValidationResult<LeaderAssignmentDto>> ExecuteAsync(int Id);
    }
}
