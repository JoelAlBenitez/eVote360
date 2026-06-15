using eVote360.Core.Domain.Common.ValidationResult;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Contracts.CandidateAssignment.Commands
{
    public interface IDeleteAssignmentCommand
    {
        Task<ValidationResult<bool>> ExecuteAsync(int assignmentId, int assigningPartyId);
    }
}
