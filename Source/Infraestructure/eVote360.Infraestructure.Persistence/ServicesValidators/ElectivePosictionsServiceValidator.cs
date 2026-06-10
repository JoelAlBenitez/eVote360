using eVote360.Core.Domain.Contracts.DomainService.ElectivePosition;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace eVote360.Infraestructure.Persistence.ServicesValidators
{
    public class ElectivePosictionsServiceValidator : IElectivePositionValidate
    {

        private readonly DbContextEVote360 _context;
        public ElectivePosictionsServiceValidator(DbContextEVote360 context)
        {
            _context = context;
        }

        public async Task<bool> ElectivePositionHasAssociatedByCandidates(int Id, string Name)
        {
            var elective =  await 
                _context.ElectivePosition
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == Id); //metodo preventivo hasta que se traigan los cambios de candidatos

            return elective != null;
        }

        public Task<bool> ElectivePositionUsedInElections(int Id, string Name)
        {
            throw new NotImplementedException(); //metodo en espera de la entidad de elecciones 
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
            //mejorar metodo para agregar actividad de elecciones
           return elective != null;
        }

        public async Task<bool> ExistsAnotherElectivePositionWithName(int Id, string Name)
        {
            var elective =  await _context.ElectivePosition
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Name == Name &&  e.Id != Id);

            return elective != null;
        }
    }
}
