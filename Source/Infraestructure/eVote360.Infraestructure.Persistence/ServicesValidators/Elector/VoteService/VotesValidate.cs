using eVote360.Core.Domain.Contracts.ServiceValidates.Elector.Votes;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace eVote360.Infraestructure.Persistence.ServicesValidators.Elector.VoteService
{
    public class VotesValidate : IVotesValidate
    {

        private readonly DbContextEVote360 _context;
        public VotesValidate(DbContextEVote360 context)
        {
            _context = context;
        }

        public async Task<bool> CitizenParticipatedInElection(Guid Id, string IdentificationCitizens)
        {
            return await _context.AuditVote.AsNoTracking().AnyAsync
                (a => a.Id == Id && a.Citizens!.IdentificationNumber.Value == IdentificationCitizens);
        }

        public async Task<bool> ElectivePositionUsedInElections(int Id)
        {
             return await _context.Vote.AsNoTracking()
                .AnyAsync(v => v.IdElectivePosiction == Id);
        }

        public async Task<bool> ExistVoteByCitizen(string identification)
        {
            return await _context.AuditVote.AsNoTracking()
               .AnyAsync(a => a.Citizens!.IdentificationNumber.Value == identification);
        }
    }
}
