using eVote360.Core.Application.DTOs.PoliticalLeaderAssignment;
using eVote360.Core.Domain.Common.ValidationResult;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Contracts.PoliticalLeaderAssignment.Commands
{
    public interface ICreateLeaderAssignmentCommand
    {
        Task<ValidationResult<LeaderAssignmentDto>> ExecuteAsync(CreateLeaderAssignmentRequestDto request, int authenticatedUserId);
    }
}
