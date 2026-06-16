using eVote360.Core.Domain.Entities.Elector.AuditVote;
using eVote360.Core.Domain.Entities.Elector.Vote;
namespace eVote360.Core.Domain.Contracts.Repositories.Elector.Vote
{
    public interface IVotingProcess
    {
        Task<bool> CreateAsync(List<Votes> votes, AuditVotes auditVotes);
        Task<IReadOnlyCollection<Votes>> GetAllVote();
    }
}
