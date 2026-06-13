using eVote360.Core.Domain.Entities.Elector.AuditVote;
using eVote360.Core.Domain.Entities.Elector.Vote;

namespace eVote360.Core.Domain.Validators.ElectorValidator.Vote
{
    public class VoteValidator : IVoteValidator
    {



        public Task<bool> ValidateCreate(Votes vote, AuditVotes auditVotes)
        {
            throw new NotImplementedException();
        }
    }
}
