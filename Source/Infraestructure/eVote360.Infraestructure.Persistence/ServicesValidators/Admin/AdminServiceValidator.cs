
using eVote360.Core.Domain.Contracts.ServiceValidates.Admin;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace eVote360.Infraestructure.Persistence.ServicesValidators.Admin
{
    public class AdminServiceValidator : IAdminFunctionalitysValidate
    {

        private readonly DbContextEVote360 _context;

        public AdminServiceValidator(DbContextEVote360 context)
        {
            _context = context;
        }
        public async Task<bool> ExisteElections()
        {
            var result =  await _context.Elections.AsNoTracking().ToListAsync();
            return result != null;
        }

        public async Task<bool> ExistElectionByYear(int year)
        {
            return await _context.Elections.AsNoTracking()
                .AnyAsync(x => x.ElectionDate.Value.Year == year);
        }
    }
}
