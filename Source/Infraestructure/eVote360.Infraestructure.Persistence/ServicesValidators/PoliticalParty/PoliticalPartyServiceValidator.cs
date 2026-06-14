using eVote360.Core.Domain.Contracts.ServiceValidates.PoliticalParty;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore; 
using PoliticalPartyEntity = eVote360.Core.Domain.Entities.PoliticalParty.PoliticalParty;

namespace eVote360.Infraestructure.Persistence.ServicesValidators.PoliticalParty
{
    public class PoliticalPartyServiceValidator : IPoliticalPartyDomainService
    {

        private readonly DbContextEVote360 _context;

    public PoliticalPartyServiceValidator(DbContextEVote360 context)
    {
        _context = context;
    }

        public async Task<bool> PoliticalPartyNameAlreadyExist(string name)
        {
            return await _context.PoliticalParties.AnyAsync(x => x.Name == name);
        }

        public async Task<bool> ValidateUniqueAcronymAsync(string acronym)
        {
            return await _context.PoliticalParties.AnyAsync(x => x.PoliticalPartyAcronym.Value == acronym);
        }

        public async Task<bool> PoliticalPartyAlreadyParticipated(int id)
        {
            return await Task.FromResult(false);
        }

        public async Task<PoliticalPartyEntity> GetByIdEntitie(int tkey)
            {
                return await _context.PoliticalParties.AsNoTracking().FirstOrDefaultAsync(x => x.Id == tkey);
            }
   
            public async Task<bool> CreateEntiteAsync(PoliticalPartyEntity entitie)
            {
                _context.PoliticalParties.Add(entitie);
                return await _context.SaveChangesAsync() > 0;
            }

            public async Task<bool> UpdateEntitieAsync(PoliticalPartyEntity entitie)
            {
                 _context.Update(entitie);
                 return await _context.SaveChangesAsync() > 0;
             }

            public async Task<bool> AlterState(int tkey, bool state)
            {
                 var party = await _context.PoliticalParties.FindAsync(tkey);
                 if (party == null) return false;
                 party.State = state;
                 return await _context.SaveChangesAsync() > 0;
             }

            public async Task<bool> ExistByNameAsync(string name) => await PoliticalPartyNameAlreadyExist(name);

            public async Task<IEnumerable<PoliticalPartyEntity>> GetActivePartiesAsync()
            {
                return await _context.PoliticalParties.Where(x => x.State).AsNoTracking().ToListAsync();
             }

            public async Task<bool> HasParticipatedInElectionsAsync(int electionPartyId) => await PoliticalPartyAlreadyParticipated(electionPartyId);

            public async Task<bool> AlterPartyStateAsync(int electionPartyId)
            {
                 var party = await _context.PoliticalParties.FindAsync(electionPartyId);
                 if (party == null) return false;
                 party.State = !party.State;
                 return await _context.SaveChangesAsync() > 0;
             }

            public async Task<IEnumerable<PoliticalPartyEntity>> GetAllAsync()
       {
                return await _context.PoliticalParties.AsNoTracking().ToListAsync();
       }

   

    }
}
