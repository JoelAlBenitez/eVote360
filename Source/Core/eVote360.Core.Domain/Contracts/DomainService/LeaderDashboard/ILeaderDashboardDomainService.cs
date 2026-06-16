using System.Threading.Tasks;

namespace eVote360.Core.Domain.Contracts.DomainService.LeaderDashboard
{
    public interface ILeaderDashboardDomainService
    {
        // 1. Información de Sesión y Partido
        Task<(int PartyId, string Name, string Acronym, string? LogoUrl)> GetPartyInfoByLeaderAsync(int userId);

        // 2. Indicadores
        Task<int> GetActiveCandidatesCountAsync(int partyId);
        Task<int> GetInactiveCandidatesCountAsync(int partyId);
        Task<int> GetApprovedAlliancesCountAsync(int partyId);
        Task<int> GetPendingAllianceRequestsCountAsync(int partyId);
        Task<int> GetAssignedCandidatesCountAsync(int partyId);
    }
}
