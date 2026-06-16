using eVote360.Core.Domain.Contracts.DomainService.LeaderDashboard;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eVote360.Infraestructure.Persistence.DomainServices
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
            // TODO: Integrar con modulo de Sebastian (Partidos y Dirigentes)
            // Por el momento simulamos la identidad del partido asignado al usuario
            return await Task.FromResult((
                PartyId: 1, 
                Name: "Partido del Pueblo", 
                Acronym: "PDP", 
                LogoUrl: "/images/logos/pdp-logo.png"
            ));
        }

        public async Task<int> GetActiveCandidatesCountAsync(int partyId)
        {
            // CONSULTA REAL (Adrian): Filtramos por partido y estado activo
            return await _context.Candidates
                .AsNoTracking()
                .CountAsync(c => c.PoliticalPartyId == partyId && c.State == true);
        }

        public async Task<int> GetInactiveCandidatesCountAsync(int partyId)
        {
            // CONSULTA REAL (Adrian): Filtramos por partido y estado inactivo
            return await _context.Candidates
                .AsNoTracking()
                .CountAsync(c => c.PoliticalPartyId == partyId && c.State == false);
        }

        public async Task<int> GetApprovedAlliancesCountAsync(int partyId)
        {
            // TODO: Integrar con modulo de Joel (Alianzas Electorales)
            // Cuando la tabla PoliticalAlliances este disponible:
            // return await _context.PoliticalAlliances.CountAsync(a => (a.SolicitantId == partyId || a.ReceiverId == partyId) && a.Status == "Aceptada");
            return await Task.FromResult(0);
        }

        public async Task<int> GetPendingAllianceRequestsCountAsync(int partyId)
        {
            // TODO: Integrar con modulo de Joel (Alianzas Electorales)
            // Solo solicitudes donde el partido del dirigente sea el receptor y este esperando respuesta
            return await Task.FromResult(0);
        }

        public async Task<int> GetAssignedCandidatesCountAsync(int partyId)
        {
            // TODO: Integrar con modulo de Joel (Asignacion de Puestos)
            // Conteo de candidatos del partido vinculados a la matriz de asignacion vigente
            return await Task.FromResult(0);
        }
    }
}
