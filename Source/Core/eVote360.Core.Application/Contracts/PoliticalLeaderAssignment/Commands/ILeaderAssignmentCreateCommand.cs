using eVote360.Core.Application.DTOs.PoliticalLeaderAssignment;
using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Contracts.PoliticalLeaderAssignment.Commands
{
    public interface  ILeaderAssignmentCreateCommand
    {
        Task<ValidationResult> ExecuteAsync(LeaderAssignmentDto dto);
    }
}
