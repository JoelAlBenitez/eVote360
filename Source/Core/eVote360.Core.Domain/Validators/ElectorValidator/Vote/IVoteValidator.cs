using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Entities.Elector.AuditVote;
using eVote360.Core.Domain.Entities.Elector.Vote;

namespace eVote360.Core.Domain.Validators.ElectorValidator.Vote
{
    public interface IVoteValidator
    {
        Task<ValidationResult> ValidateCreate(List<Votes> votes, AuditVotes auditVotes);
        Task<ValidationResult> ValidateStates(List<Votes> votes, AuditVotes auditVotes);
    }
}
