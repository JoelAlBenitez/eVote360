using eVote360.Core.Application.DTOs.CandidateAssignment;
using eVote360.Core.Domain.Common.ValidationResult;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Contracts.CandidateAssignment.Query
{
    public interface IGetAssignmentByIdQuery
    {
        Task<ValidationResult<CandidateAssignmentDto>> ExecuteAsync(int assignmentId);
    }
}
