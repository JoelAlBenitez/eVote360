using eVote360.Core.Domain.Contracts.ServiceValidates.Citizens;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace eVote360.Infraestructure.Persistence.ServicesValidators.Citizens
{
    public class CitizensServicesValidate : ICitizensServiceValidate
    {

        private readonly DbContextEVote360 _context;

        public CitizensServicesValidate(DbContextEVote360 context)
        {
            _context = context;
        }


        public async Task<bool> CitizentHasAssociatedEmail(Guid Id)
        {
            var result = await _context.Citzens
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == Id);
            
            return result!.Email.Value != null ? true : false;
        }

        public async Task<bool> CurrentStateCitizen(Guid Id)
        {
            var result = await _context.Citzens
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == Id);
            if (result == null) return false;
            return result.State;
        }

        public async Task<bool> CurrentStateOfTheCitizen(Guid? Id, string? Identification = null)
        {
            if(Identification != null)
            {
                var state = await _context.Citzens
               .AsNoTracking()
               .FirstOrDefaultAsync(c => c.IdentificationNumber.Value == Identification);
                return state!.State;
            }

            var citizen = await _context.Citzens
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == Id);
            return citizen!.State;
        }

        public async Task<bool> ExistByIdCitizen(Guid Id)
        {
            var citizen = await _context.Citzens
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == Id);

            return citizen != null;
        }

        public async Task<bool> ExistCitizensByEmail(string Email, Guid? Id)
        {
            if (Id == null)
            {
                var citizen = await _context.Citzens
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Email.Value == Email);
                return citizen != null;
            }
            else
            {
                var citizen = await _context.Citzens
                   .AsNoTracking()
                   .FirstOrDefaultAsync(x => x.Email.Value == Email &&  x.Id != Id );
                return citizen != null;
            }
        }

        public  async Task<bool> ExistCitizensByIdentification(string Identification)
        {
            var citizen = await _context.Citzens
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdentificationNumber.Value == Identification);
            return citizen != null;
        }

        public async Task<bool> ExistOtherCitizens(Guid Id, string Identification)
        {
            var citizen = await _context.Citzens.AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdentificationNumber.Value == Identification && x.Id != Id);
            return citizen != null;
        }

        public async Task<bool> ExistOtherCitizensByState(Guid Id, string Identification, bool state)
        {
          return await _context
                .Citzens
                .AsNoTracking()
                .AnyAsync(x => x.Id != Id && x.IdentificationNumber.Value != Identification && x.State == state);

            
        }
    }
}
