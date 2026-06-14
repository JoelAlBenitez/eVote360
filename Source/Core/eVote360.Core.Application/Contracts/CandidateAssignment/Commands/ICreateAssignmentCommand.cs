using eVote360.Core.Application.DTOs.CandidateAssignment;
using eVote360.Core.Domain.Common.ValidationResult;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Contracts.CandidateAssignment.Commands
{
    public interface ICreateAssignmentCommand
    {
        Task<ValidationResult<CandidateAssignmentDto>> ExecuteAsync(CreateAssignmentRequestDto request, int assigningPartyId, int authenticatedUserId);
    }
}
