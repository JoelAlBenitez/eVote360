using eVote360.Core.Application.Contracts.LeaderDashboard.Query;
using eVote360.Core.Application.DTOs.LeaderDashboard;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.DomainService.LeaderDashboard;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Services.LeaderDashboard.Query
{
    public class LeaderDashboardGetQueryHandler : ILeaderDashboardGetQuery
    {
        private readonly ILeaderDashboardDomainService _dashboardDomainService;
        private List<Error> _errors = new List<Error>();

        public LeaderDashboardGetQueryHandler(ILeaderDashboardDomainService dashboardDomainService)
        {
            _dashboardDomainService = dashboardDomainService;
        }

        public async Task<ValidationResult<LeaderDashboardDto>> GetDashboardDataAsync(int userId)
        {
            try
            {
                var partyInfo = await _dashboardDomainService.GetPartyInfoByLeaderAsync(userId);

                if (partyInfo.PartyId == 0)
                {
                    _errors.Add(new Error("Acceso Denegado", "El dirigente no tiene un partido válido asignado."));
                    return ValidationResult<LeaderDashboardDto>.Failure(_errors);
                }

                var activeCandidates = await _dashboardDomainService.GetActiveCandidatesCountAsync(partyInfo.PartyId);
                var inactiveCandidates = await _dashboardDomainService.GetInactiveCandidatesCountAsync(partyInfo.PartyId);
                var approvedAlliances = await _dashboardDomainService.GetApprovedAlliancesCountAsync(partyInfo.PartyId);
                var pendingRequests = await _dashboardDomainService.GetPendingAllianceRequestsCountAsync(partyInfo.PartyId);
                var assignedCandidates = await _dashboardDomainService.GetAssignedCandidatesCountAsync(partyInfo.PartyId);

                var dto = new LeaderDashboardDto
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

                return ValidationResult<LeaderDashboardDto>.Success(dto);
            }
            catch (Exception ex)
            {
                _errors.Add(new Error("Error inesperado", ex.Message));
                return ValidationResult<LeaderDashboardDto>.Failure(_errors);
            }
        }
    }
}
