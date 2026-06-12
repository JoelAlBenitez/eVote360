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

        public Task<bool> CitizenParticipatedInElection(Guid Id, string Identification)
        {
            throw new NotImplementedException(); //en espera de la entidad de elecciones y votos 
        }

        public async Task<bool> CurrentStateOfTheCitizen(Guid Id)
        {
            var citizen = await _context.Citzens
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == Id);
            return citizen!.State;
        }

        public async Task<bool> ExistCitizensByEmail(string Email)
        {
            var citizen = await _context.Citzens
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email.Value == Email);
            return citizen != null;
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
            var citizen = await _context
                .Citzens
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == Id && x.IdentificationNumber.Value == Identification && x.State == state);
            return citizen != null;
        }
    }
}
