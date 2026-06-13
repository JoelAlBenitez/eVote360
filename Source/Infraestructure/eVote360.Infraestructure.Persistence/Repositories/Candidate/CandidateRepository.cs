using eVote360.Core.Domain.Contracts.Repositories.Candidate;
using eVote360.Core.Domain.Entities.Candidate;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVote360.Infraestructure.Persistence.Repositories.Candidate
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly DbContextEVote360 _context;

        public CandidateRepository(DbContextEVote360 context)
        {
            _context = context;
        }

        public async Task<bool> CreateEntiteAsync(Candidates entitie)
        {
            await _context.Set<Candidates>().AddAsync(entitie);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateEntitieAsync(Candidates entitie)
        {
            _context.Set<Candidates>().Update(entitie);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Candidates> GetByIdEntitie(int tkey)
        {
            return await _context.Candidates
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == tkey) ?? null!;
        }

        public async Task<bool> AlterState(int tkey, bool state)
        {
            var entity = await _context.Candidates
                .FirstOrDefaultAsync(x => x.Id == tkey);

            if (entity == null) return false;
            entity.State = state;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Candidates>> GetAllByPartyIdAsync(int partyId)
        {
            return await _context.Set<Candidates>()
                .Where(x => x.PoliticalPartyId == partyId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
