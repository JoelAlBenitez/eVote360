using eVote360.Core.Domain.Contracts.Repositories.Elector.Otp;
using eVote360.Core.Domain.Entities.Elector.CodeVerifications;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace eVote360.Infraestructure.Persistence.Repositories.Elector.OTPCODE
{
    public class OtpRepository : IOtpRepository
    {

        private readonly DbContextEVote360 _context;
        public OtpRepository(DbContextEVote360 context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(CodeVerification codeVerification)
        {
             await _context.CodeVerifications.AddAsync(codeVerification);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<CodeVerification> GetByIdAndIdCitizens(Guid IdCitizens, int IdElection)
        {
            return (await _context.CodeVerifications
                .AsNoTracking()
                .FirstOrDefaultAsync(code => code.IdCitizens == IdCitizens && code.IdElection == IdElection))!;
        }
    }
}
