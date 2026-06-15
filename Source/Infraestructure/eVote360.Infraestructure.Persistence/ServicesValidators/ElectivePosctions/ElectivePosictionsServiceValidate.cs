using eVote360.Core.Domain.Contracts.DomainService.ElectivePosition;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace eVote360.Infraestructure.Persistence.ServicesValidators.ElectivePosctions
{
    public class ElectivePosictionsServiceValidate : IElectivePositionValidate
    {

        private readonly DbContextEVote360 _context;
        public ElectivePosictionsServiceValidate(DbContextEVote360 context)
        {
            _context = context;
        }

        public async Task<bool> CurrentStateElectivePosiction(int Id)
        {
            var result = await _context.ElectivePosition
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == Id);
            return result!.State;
        }

        public async Task<bool> ElectivePositionHasAssociatedByCandidates(int Id)
        {


           

            return await _context.Candidates.AsNoTracking()
                    .AnyAsync(c => c.electivePositions.Id == Id);
                
        }

        public async Task<bool> ElectivePositionUsedInElections(int Id)
        {

            
              return  await _context.Vote
                .AsNoTracking()
                .AnyAsync(e => e.IdElectivePosiction  == Id);
             
           
        }

        public async Task<bool> ExistById(int Id)
        {
            return await _context.ElectivePosition
                .AsNoTracking()
                .AnyAsync(x => x.Id == Id);
        }

        public async Task<bool> ExistElectivePositionByName(string Name)
        {
            var elective = await _context.ElectivePosition
                .AsNoTracking()
                .FirstOrDefaultAsync (x => x.Name == Name);
            return elective != null;
        }

        public async Task<bool> ExistElectivePositionByState(int Id, string Name, bool State)
        {
            var elective = await _context.ElectivePosition
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Name == Name && e.Id != Id && e.State != State);
           return elective != null;
        }

        public async Task<bool> ExistsAnotherElectivePositionWithName(int Id, string Name)
        {
            return await _context.ElectivePosition
                .AsNoTracking()
                .AnyAsync(e => e.Name == Name &&  e.Id != Id);

           
        }
    }
}
