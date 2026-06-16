using eVote360.Core.Domain.Contracts.ServiceValidates.Elector.CodeVerifications;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace eVote360.Infraestructure.Persistence.ServicesValidators.Elector.CodeVerification
{
    public class CodeVerificationValidate : ICodeVerificationValidate
    {

        public DbContextEVote360 _context;
        public CodeVerificationValidate(DbContextEVote360 context)
        {
            _context = context;
        }

        public async Task<bool> CodeExpire(Guid IdCitizen, int IdElection)
        {
            var result = await _context.CodeVerifications.AsNoTracking()
                .FirstOrDefaultAsync(c => c.IdCitizens == IdCitizen && c.IdElection == IdElection);
            if (result == null) return true;
            return DateTime.UtcNow >= result.ExpirationDate;
        }

        public async Task<bool> CodeMatchesWithRecord(int code, Guid IdCitizen, int IdElection)
        {
            return await _context.CodeVerifications.AsNoTracking()
                 .AnyAsync(c => c.IdCitizens == IdCitizen && c.IdElection == IdElection && c.Code == code);
        }

        public async Task<bool> CodeUse(Guid IdCitizen, int IdElection)
        {
            var result =  await _context.CodeVerifications.AsNoTracking()
                .FirstOrDefaultAsync(c => c.IdCitizens == IdCitizen &&  c.IdElection == IdElection);
            if(result == null) return true;
            return result.State;
        }

        public async Task<bool> ExistCodeVerification(Guid IdCitizen, int IdElection)
        {
            return await _context.CodeVerifications.AsNoTracking()
                .AnyAsync(c => c.IdCitizens == IdCitizen && c.IdElection == IdElection);
        }

        public async Task<bool> ExistCodeVerificationActive(Guid IdCitizen, int IdElection)
        {
            return await _context.CodeVerifications.AsNoTracking()
                .AnyAsync(c => c.IdCitizens == IdCitizen && c.IdElection == IdElection && c.ExpirationDate < DateTime.UtcNow);
        }
    }
}
