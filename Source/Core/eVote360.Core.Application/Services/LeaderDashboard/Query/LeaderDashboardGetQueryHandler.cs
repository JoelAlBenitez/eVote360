using eVote360.Core.Application.Contracts.LeaderDashboard.Query;
using eVote360.Core.Application.DTOs.LeaderDashboard;
using eVote360.Core.Domain.Contracts.DomainService.LeaderDashboard;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Services.LeaderDashboard.Query
{
    public class LeaderDashboardGetQueryHandler : ILeaderDashboardGetQuery
    {
        private readonly ILeaderDashboardDomainService _dashboardDomainService;

        public LeaderDashboardGetQueryHandler(ILeaderDashboardDomainService dashboardDomainService)
        {
            _dashboardDomainService = dashboardDomainService;
        }

        public async Task<LeaderDashboardDto> GetDashboardDataAsync(int userId)
        {
            try
            {
                // 1. Validamos al usuario y obtenemos la informacion de su partido
                var partyInfo = await _dashboardDomainService.GetPartyInfoByLeaderAsync(userId);

                // Si el usuario no es un dirigente valido o no tiene partido asignado, 
                // devolvemos nulo (el controlador deberia retornar un 403 Forbidden o similarr
                if (partyInfo.PartyId == 0)
                {
                    return null;
                }

                // Teniend el PartyId asegurado, hacemos toas las consultas usando ese ID
                // Cumpliendo asi con la "Regla de Oro de Seguridad" del documento de leoo
                var activeCandidates = await _dashboardDomainService.GetActiveCandidatesCountAsync(partyInfo.PartyId);
                var inactiveCandidates = await _dashboardDomainService.GetInactiveCandidatesCountAsync(partyInfo.PartyId);
                var approvedAlliances = await _dashboardDomainService.GetApprovedAlliancesCountAsync(partyInfo.PartyId);
                var pendingRequests = await _dashboardDomainService.GetPendingAllianceRequestsCountAsync(partyInfo.PartyId);
                var assignedCandidates = await _dashboardDomainService.GetAssignedCandidatesCountAsync(partyInfo.PartyId);

               
                return new LeaderDashboardDto
                {
                    PartyName = partyInfo.Name,
                    PartyAcronym = partyInfo.Acronym,
                    PartyLogoUrl = partyInfo.LogoUrl,
                    ActiveCandidatesCount = activeCandidates,
                    InactiveCandidatesCount = inactiveCandidates,
                    ApprovedAlliancesCount = approvedAlliances,
                    PendingAllianceRequestsCount = pendingRequests,
                    AssignedCandidatesToPositionsCount = assignedCandidates
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
