using eVote360.Core.Domain.Contracts.Repositories.AdminManager;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace eVote360.Infraestructure.Persistence.Repositories.Admin
{
    public class AdminRepository : IAdminManagerRepository
    {
        private readonly DbContextEVote360 _context;

        public AdminRepository(DbContextEVote360 context)
        {
            _context = context;
        }

        public async Task<int> CountCandidactsRegisterAsync()
        {
            var result = await _context.Candidates.AsNoTracking().ToListAsync();
            return result != null ? result.Count : 0;
        }

        public async Task<int> CountCitizensRegisterAsync()
        {
            var result = await _context.Citzens.AsNoTracking().ToListAsync();
            return result != null ? result.Count : 0;
        }

        public async Task<int> CountElectionsRegisterAsync()
        {
            var result = await _context.Elections.AsNoTracking().ToListAsync();
            return result != null ? result.Count : 0;
        }


        public async Task<IReadOnlyCollection<int>> GetYears()
        {
            var years = await _context.Elections
                .AsNoTracking()
                .Select(e => e.ElectionDate.Value.Year)
                .Distinct()
                .OrderByDescending(years => years)
                .ToListAsync();
            return years;
        }

        public async Task<int> PoliticalPartyAsync()
        {
            var result = await _context.PoliticalParties.AsNoTracking().ToListAsync();
            return result != null ? result.Count : 0;
        }
    }
}
