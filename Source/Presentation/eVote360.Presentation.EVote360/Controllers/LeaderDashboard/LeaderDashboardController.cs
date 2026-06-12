using eVote360.Core.Application.Contracts.LeaderDashboard.Query;
using eVote360.Core.Application.ViewModels.LeaderDashboard;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eVote360.Presentation.EVote360.Controllers.LeaderDashboard
{
    public class LeaderDashboardController : Controller
    {
        private readonly ILeaderDashboardGetQuery _leaderDashboardGetQuery;

        // TODO: Obtener el ID del usuario desde la sesión/cookie del dirigente autenticado
        // Por ahora hardcodeamos 1 para mantener el flujo funcional y seguro.
        private readonly int _currentUserId = 1;

        public LeaderDashboardController(ILeaderDashboardGetQuery leaderDashboardGetQuery)
        {
            _leaderDashboardGetQuery = leaderDashboardGetQuery;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _leaderDashboardGetQuery.GetDashboardDataAsync(_currentUserId);

            // Si falla (ej. el usuario no es dirigente o no tiene partido asignado)
            if (!result.IsValid)
            {
                foreach (var item in result.errors)
                {
                    // Mostrar el error en la pantalla (Aviso de "Acceso Denegado" o similar)
                    ModelState.AddModelError(item.Code, item.Description);
                }
                
                // Retornamos la vista vacia o podriamos redirigir a un "Home" general
                return View(new LeaderDashboardViewModel()); 
            }

            // Si es exitoso, mapeamos el DTO al ViewModel
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
