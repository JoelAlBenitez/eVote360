
using eVote360.Core.Domain.Entities.Elector.AuditVote;
using eVote360.Core.Domain.Entities.Elector.Vote;

namespace eVote360.Core.Domain.Validators.ElectorValidator.Vote
{
    public interface IVoteValidator
    {
        Task<bool> ValidateCreate(Votes vote, AuditVotes auditVotes);
    }
}
