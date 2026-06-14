using eVote360.Core.Domain.Contracts.Repositories.PoliticalAlliences;
using eVote360.Core.Domain.Common.Enums;
using PoliticalAllianceEntity = eVote360.Core.Domain.Entities.PoliticalAlliances.PoliticalAlliances;
using eVote360.Core.Domain.Entities.PoliticalAlliances;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVote360.Infraestructure.Persistence.Repositories.PoliticalAlliances
{
    public class PoliticalAlliancesRepository : IPoliticalAllienceRepository
    {
        private readonly DbContextEVote360 _context;

        public PoliticalAlliancesRepository(DbContextEVote360 context)
        {
            _context = context;
        }

        public async Task<PoliticalAllianceEntity> GetByIdEntitie(int tkey)
        {
            var alliance = await _context.PoliticalAlliances
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == tkey);
            
            return alliance!;
        }

        public async Task<bool> CreateEntiteAsync(PoliticalAllianceEntity entitie)
        {
            _context.PoliticalAlliances.Add(entitie);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateEntitieAsync(PoliticalAllianceEntity entitie)
        {
            _context.PoliticalAlliances.Update(entitie);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int allianceId)
        {
            var alliance = await _context.PoliticalAlliances
                .FirstOrDefaultAsync(x => x.Id == allianceId);
            
            if (alliance == null) return false;

            _context.PoliticalAlliances.Remove(alliance);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AlterState(int tkey, bool state)
        {
            
            return await Task.FromResult(false);
        }

        public async Task<IEnumerable<PoliticalAllianceEntity>> GetPendingReceivedAsync(int partyId)
        {
            return await _context.PoliticalAlliances
                .AsNoTracking()
                .Where(x => x.ReceivingPartyId == partyId && x.Status == AllianceStatus.Pending)
                .ToListAsync();
        }

        public async Task<IEnumerable<PoliticalAllianceEntity>> GetSentRequestsAsync(int partyId)
        {
            return await _context.PoliticalAlliances
                .AsNoTracking()
                .Where(x => x.RequestingPartyId == partyId)
                .ToListAsync();
        }

        public async Task<IEnumerable<PoliticalAllianceEntity>> GetActiveAlliancesAsync(int partyId)
        {
            return await _context.PoliticalAlliances
                .AsNoTracking()
                .Where(x => x.Status == AllianceStatus.Accepted && 
                           (x.RequestingPartyId == partyId || x.ReceivingPartyId == partyId))
                .ToListAsync();
        }
    }
}
