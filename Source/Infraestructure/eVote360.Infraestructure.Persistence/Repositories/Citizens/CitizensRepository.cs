using eVote360.Core.Domain.Contracts.Repositories.Citizens;
using eVote360.Core.Domain.Entities.Citizens;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace eVote360.Infraestructure.Persistence.Repositories.Citizens
{
    public class CitizensRepository : ICitizenRepository
    {

        private readonly DbContextEVote360 _context;

        public CitizensRepository(DbContextEVote360 context)
        {
            _context = context;
        }

        public async Task<bool> AlterState(Guid tkey, bool state)
        {
             var citizens = await _context.Citzens.
                AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == tkey);
             if(citizens == null)  return false; 
               citizens.State = state;
             _context.Update(citizens) ;
            return await _context.SaveChangesAsync()  > 0;
        }

        public async Task<bool> CreateEntiteAsync(Citizen entitie)
        {
            _context.Citzens.Add(entitie);
            return await _context.SaveChangesAsync()> 0 ;
        }

        public async Task<IReadOnlyCollection<Citizen>> GetActiveCitizensByActive()
        {
             var citizens = await _context.Citzens.Where(c => c.State == true)
                .AsNoTracking().ToListAsync();
            if (citizens == null) return new List<Citizen>();
            return citizens;
        }

        public async Task<IReadOnlyCollection<Citizen>> GetAll()
        {
            var citizens = await _context.Citzens
               .AsNoTracking().ToListAsync();
            if (citizens == null) return new List<Citizen>();
            return citizens;
        }

        public async Task<Citizen> GetByIdentification(string Identification)
        {
            var result = await _context.Citzens
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.IdentificationNumber.Value == Identification);
            return result!;
        }

        public async Task<Citizen> GetByIdEntitie(Guid tkey)
        {
            var citizen  = await _context.Citzens.AsNoTracking().FirstOrDefaultAsync(x => x.Id == tkey);
            return citizen!;
        }

        public async Task<bool> UpdateEntitieAsync(Citizen entitie)
        {
            var existing = await _context.Citzens.FindAsync(entitie.Id);
            if (existing == null) return false;

            existing.Name = entitie.Name;
            existing.LastName = entitie.LastName;
            existing.Email = entitie.Email;
            existing.IdentificationNumber = entitie.IdentificationNumber;
            existing.State = entitie.State;
            existing.UpdateAt = entitie.UpdateAt;
            existing.UpdateUserId = entitie.UpdateUserId;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
