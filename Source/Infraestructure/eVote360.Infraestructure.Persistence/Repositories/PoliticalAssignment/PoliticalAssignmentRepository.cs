using eVote360.Core.Domain.Contracts.Repositories.PoliticalAssignment;
using eVote360.Core.Domain.Entities.PoliticalParty;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using AssignmentEntity = eVote360.Core.Domain.Entities.PoliticalAssignment.PoliticalAssignment;
namespace eVote360.Infraestructure.Persistence.Repositories.PoliticalAssignment
{
    public class PoliticalAssignmentRepository : IPoliticalAssignmentRepository
    {
        protected readonly DbContextEVote360 _context;

        public PoliticalAssignmentRepository(DbContextEVote360 context) {
        _context = context;
        }

        public async Task<bool> CreateEntiteAsync(AssignmentEntity entitie)
            {
                _context.PoliticalAssignments.Add(entitie);
                return await _context.SaveChangesAsync() > 0;
            }
   
            public async Task<AssignmentEntity> GetByIdEntitie(int tkey)
            {
                return await _context.PoliticalAssignments
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == tkey);
            }

            public async Task<bool> UpdateEntitieAsync(AssignmentEntity entitie)
            {
                 _context.Update(entitie);
                 return await _context.SaveChangesAsync() > 0;
             }

            public async Task<bool> AlterState(int tkey, bool state)
            {
                 var assignment = await _context.PoliticalAssignments.FindAsync(tkey);
                 if (assignment == null) return false;
   
                assignment.State = state;
                 _context.Update(assignment);
                 return await _context.SaveChangesAsync() > 0;
             }
   
            public async Task<bool> HasAlreadyAssignmentAsync(int politicalLeaderId, int politicalPartyId)
            {
                return await _context.PoliticalAssignments
                 .AnyAsync(x => x.PoliticalLeaderId == politicalLeaderId &&
                                   x.PoliticalPartyId == politicalPartyId &&
                                   x.State == true);
             }

            public async Task<IEnumerable> GetAllLeadersFromPartyAsync(int politicalPartyId)
            {
                return await _context.PoliticalAssignments
                 .AsNoTracking()
                 .Where(x => x.PoliticalPartyId == politicalPartyId && x.State == true)
                 .ToListAsync();
            }

            public async Task<IReadOnlyCollection<AssignmentEntity>> GetAllAsync()
            {
                 return await _context.PoliticalAssignments
                 .AsNoTracking()
                 .ToListAsync();
            }


    }
}
