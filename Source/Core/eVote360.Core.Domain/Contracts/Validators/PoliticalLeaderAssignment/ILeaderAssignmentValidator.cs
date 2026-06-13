using eVote360.Core.Domain.Common.ValidationResult;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Contracts.Validators.PoliticalLeaderAssignment
{
    public interface ILeaderAssignmentValidator
    {
        Task<ValidationResult> ValidateCreateAsync(int userId, int partyId);
        Task<ValidationResult> ValidateDeleteAsync(int assignmentId);
    }
}
