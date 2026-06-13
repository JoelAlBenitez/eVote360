using eVote360.Core.Domain.Common.ValidationResult;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Contracts.PoliticalLeaderAssignment.Commands
{
    public interface IDeleteLeaderAssignmentCommand
    {
        Task<ValidationResult<bool>> ExecuteAsync(int assignmentId);
    }
}
