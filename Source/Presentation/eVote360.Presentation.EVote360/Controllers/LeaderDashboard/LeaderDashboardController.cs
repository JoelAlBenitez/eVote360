using eVote360.Core.Application.Contracts.Authentication.Command;
using eVote360.Core.Application.Contracts.LeaderDashboard.Query;
using eVote360.Core.Application.ViewModels.LeaderDashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eVote360.Presentation.EVote360.Controllers.LeaderDashboard
{
    [Authorize(Roles = "DirigentePolitico")]
    public class LeaderDashboardController : Controller
    {
        private readonly ILeaderDashboardGetQuery _leaderDashboardGetQuery;
        private readonly ISessionUser _sessionUser;

        private int GetUserId() => _sessionUser.GetUserId();
        private int GetPartyId() => _sessionUser.GetPoliticalParty();

        public LeaderDashboardController(ILeaderDashboardGetQuery leaderDashboardGetQuery, ISessionUser sessionUser)
        {
            _leaderDashboardGetQuery = leaderDashboardGetQuery;
            _sessionUser = sessionUser;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _leaderDashboardGetQuery.GetDashboardDataAsync(GetUserId());

            if (!result.IsValid)
            {
                foreach (var item in result.errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                
                return View(new LeaderDashboardViewModel()); 
            }

            var vm = new LeaderDashboardViewModel
            {
                PartyName = result.Value!.PartyName,
                PartyAcronym = result.Value.PartyAcronym,
                PartyLogoUrl = result.Value.PartyLogoUrl,
                ActiveCandidatesCount = result.Value.ActiveCandidatesCount,
                InactiveCandidatesCount = result.Value.InactiveCandidatesCount,
                ApprovedAlliancesCount = result.Value.ApprovedAlliancesCount,
                PendingAllianceRequestsCount = result.Value.PendingAllianceRequestsCount,
                AssignedCandidatesToPositionsCount = result.Value.AssignedCandidatesToPositionsCount
            };

            return View(vm);
        }
    }
}
