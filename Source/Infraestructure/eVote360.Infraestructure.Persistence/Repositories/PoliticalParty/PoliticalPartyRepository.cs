using eVote360.Core.Domain.Contracts.Repositories.PoliticalParty;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using PoliticalPartyEntity = eVote360.Core.Domain.Entities.PoliticalParty.PoliticalParty;

namespace eVote360.Infraestructure.Persistence.Repositories.PoliticalParty
{
    public class PoliticalPartyRepository : IPoliticalPartyRepository
    {
        protected readonly DbContextEVote360 _context;

        public PoliticalPartyRepository(DbContextEVote360 context) {
        _context = context;
        }

        public async Task<bool> CreateEntiteAsync(PoliticalPartyEntity entitie)
        {
            _context.PoliticalParties.Add(entitie);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PoliticalPartyEntity> GetByIdEntitie(int tkey)
        {
            return await _context.PoliticalParties
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == tkey);
        }

        public async Task<bool> UpdateEntitieAsync(PoliticalPartyEntity entitie)
        {
            var existing = await _context.PoliticalParties.FindAsync(entitie.Id);
            if (existing == null) return false;

            existing.Name = entitie.Name;
            existing.PoliticalPartyDescription = entitie.PoliticalPartyDescription;
            existing.PoliticalPartyLogo = entitie.PoliticalPartyLogo;
            existing.PoliticalPartyAcronym = entitie.PoliticalPartyAcronym;
            existing.State = entitie.State;
            existing.UpdateAt = entitie.UpdateAt;
            existing.UpdateUserId = entitie.UpdateUserId;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AlterState(int tkey, bool state)
        {
            var party = await _context.PoliticalParties.FindAsync(tkey);
            if (party == null) return false;

            party.State = state;
            _context.PoliticalParties.Update(party);
            return await _context.SaveChangesAsync() > 0;
        }


            public async Task<bool> ExistByNameAsync(string name)
            {
                return await _context.PoliticalParties.AnyAsync(x => x.Name == name);
            }
   
            public async Task<bool> ValidateUniqueAcronymAsync(string acronym)
            {
                return await _context.PoliticalParties.AnyAsync(x => x.PoliticalPartyAcronym.Value == acronym);
            }

            public async Task<IEnumerable<PoliticalPartyEntity>> GetActivePartiesAsync()
            {
                 return await _context.PoliticalParties
                .AsNoTracking()
                .Where(x => x.State == true)
                .ToListAsync();
             }

            public async Task<IEnumerable<PoliticalPartyEntity>> GetAllAsync()
            {
                 return await _context.PoliticalParties
                .AsNoTracking()
                .ToListAsync();
             }

            public async Task<bool> HasParticipatedInElectionsAsync(int electionPartyId)
            {
                //me falta integrar con elecciones
                return await Task.FromResult(false);
             }

            public async Task<bool> AlterPartyStateAsync(int electionPartyId)
            {
                 var party = await _context.PoliticalParties.FindAsync(electionPartyId);
                 if (party == null) return false;
    
                 party.State = !party.State; 
                 _context.PoliticalParties.Update(party);
                 return await _context.SaveChangesAsync() > 0;
             }

    }
}
