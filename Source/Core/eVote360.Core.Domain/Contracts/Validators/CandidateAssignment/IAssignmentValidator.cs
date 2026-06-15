using eVote360.Core.Domain.Common.ValidationResult;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Contracts.Validators.CandidateAssignment
{
    public interface IAssignmentValidator
    {
        Task<ValidationResult> ValidateCreateAsync(int candidateId, int electivePositionId, int assigningPartyId);
        Task<ValidationResult> ValidateDeleteAsync(int assignmentId, int assigningPartyId);
    }
}
