using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Contracts.DomainService.LeaderDashboard;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
namespace eVote360.Infraestructure.Persistence.ServicesValidators.LeaderDashboard
{
    public class LeaderDashboardDomainService : ILeaderDashboardDomainService
    {
        private readonly DbContextEVote360 _context;
        public LeaderDashboardDomainService(DbContextEVote360 context)
        {
            _context = context;
        }
        public async Task<(int PartyId, string Name, string Acronym, string? LogoUrl)> GetPartyInfoByLeaderAsync(int userId)
        {
            var assignment = await _context.PoliticalAssignments
                .AsNoTracking()
                .Include(a => a.PoliticalParty)
                .Where(a => a.PoliticalLeaderId == userId)
                .OrderByDescending(a => a.PolitcalAssignmentDate)
                .FirstOrDefaultAsync();

            if (assignment == null || assignment.PoliticalParty == null)
            {
                return (PartyId: 0, Name: string.Empty, Acronym: string.Empty, LogoUrl: null);
            }

            return (
             PartyId: assignment.PoliticalPartyId,
             Name: assignment.PoliticalParty.Name,
             Acronym: assignment.PoliticalParty.PoliticalPartyAcronym.Value.ToString() ?? "Desconocido",
             LogoUrl: assignment.PoliticalParty.PoliticalPartyLogo.PhotoUrl
         );
                }
        public async Task<int> GetActiveCandidatesCountAsync(int partyId)
        {
            return await _context.Candidates
                .AsNoTracking()
                .CountAsync(c => c.PoliticalPartyId == partyId && c.State == true);
        }
        public async Task<int> GetInactiveCandidatesCountAsync(int partyId)
        {
            return await _context.Candidates
                .AsNoTracking()
                .CountAsync(c => c.PoliticalPartyId == partyId && c.State == false);
        }
        public async Task<int> GetApprovedAlliancesCountAsync(int partyId)
        {
            return await _context.PoliticalAlliances
                .AsNoTracking()
                .CountAsync(a => (a.RequestingPartyId == partyId || a.ReceivingPartyId == partyId)
                    && a.Status == AllianceStatus.Aceptado);
        }
        public async Task<int> GetPendingAllianceRequestsCountAsync(int partyId)
        {
            // Solo solicitudes donde el partido del dirigente es el RECEPTOR
            // y está esperando respuesta (no las que él mismo envió)
            return await _context.PoliticalAlliances
                .AsNoTracking()
                .CountAsync(a => a.ReceivingPartyId == partyId
                    && a.Status == AllianceStatus.Pendiente);
        }
        public async Task<int> GetAssignedCandidatesCountAsync(int partyId)
        {
            return await _context.CandidateAssignments
                .AsNoTracking()
                .CountAsync(ca => ca.AssigningPartyId == partyId);
        }
    }
}