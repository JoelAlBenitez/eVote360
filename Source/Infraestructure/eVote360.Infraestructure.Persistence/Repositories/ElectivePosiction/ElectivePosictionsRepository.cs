using eVote360.Core.Domain.Contracts.Repositories.ElectivePosition;
using eVote360.Core.Domain.Entities.ElectivePosition;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
namespace eVote360.Infraestructure.Persistence.Repositories.ElectivePosiction
{
    public class ElectivePosictionsRepository : IElectivePositionsRepository
    {

        private readonly DbContextEVote360 _context;
        public ElectivePosictionsRepository(DbContextEVote360 context)
        {
            _context = context;
        }

        public async Task<bool> AlterState(int tkey, bool state)
        {
            var elective = await _context.ElectivePosition
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == tkey);
            if(elective == null)
                return false;   
            elective.State = state;
            _context.ElectivePosition.Update(elective);
            return _context.SaveChanges()  > 0;
        }

        public async Task<bool> CreateEntiteAsync(ElectivePositions entitie)
        {
             _context.ElectivePosition.Add(entitie);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IReadOnlyCollection<ElectivePositions>> GetAllActiveAsync()
        {
            return await _context.ElectivePosition.Where(x => x.State)
              .AsNoTracking().
              ToListAsync();
                
        }

        public async Task<IReadOnlyCollection<ElectivePositions>> GetAllAsync()
        {
            return await _context.ElectivePosition.AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyCollection<ElectivePositions>> GetAllDateAsync(DateTimeOffset? dateStart, DateTimeOffset? dateEnd)
        {
            return await _context.ElectivePosition.Where(x => x.CreateAt >= dateStart && x.CreateAt <= dateEnd)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ElectivePositions> GetByIdEntitie(int tkey)
        {
            var elective = await _context.ElectivePosition
                  .AsNoTracking()
                  .FirstOrDefaultAsync(x => x.Id == tkey);
            if (elective == null)
                return null!;
            return elective;
        }

        public async Task<bool> UpdateEntitieAsync(ElectivePositions entitie)
        {
            _context.ElectivePosition.Update(entitie);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
