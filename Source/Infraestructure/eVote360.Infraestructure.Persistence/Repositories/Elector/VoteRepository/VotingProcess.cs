using eVote360.Core.Domain.Entities.Elector.AuditVote;
using eVote360.Core.Domain.Entities.Elector.Vote;
using eVote360.Core.Domain.Contracts.Repositories.Elector.Vote;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace eVote360.Infraestructure.Persistence.Repositories.Elector.VoteRepository
{
    public class VotingProcess : IVotingProcess
    {
        private readonly DbContextEVote360 _context;
        public VotingProcess(DbContextEVote360 context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Votes votes, AuditVotes auditVotes)
        {
            await _context.Vote.AddAsync(votes);
            await _context.AuditVote.AddAsync(auditVotes);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IReadOnlyCollection<Votes>> GetAllVote()
        {
            return await _context.Vote.AsNoTracking().ToListAsync();
        }
    }
}
