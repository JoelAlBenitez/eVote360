using eVote360.Core.Application.Contracts.Admin.Query;
using eVote360.Core.Application.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace eVote360.Presentation.EVote360.Controllers.Admin
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly IAvailableYearsQuery _availableYearsQuery;
        private readonly ICountRegisterAdminQuery _countRegisterAdminQuery;
        private readonly IElectionByYearQuery _electionByYearQuery;

        public AdminController(IElectionByYearQuery electionByYearQuery,
            IAvailableYearsQuery availableYearsQuery,
            ICountRegisterAdminQuery countRegisterAdminQuery
            )
        {
            _availableYearsQuery = availableYearsQuery;
            _countRegisterAdminQuery = countRegisterAdminQuery;
            _electionByYearQuery = electionByYearQuery;
        }

        public async Task<IActionResult> Index()
        {
            var  admin = await _countRegisterAdminQuery.CountRegisterQueryAsync();
            var view = new AdminViewModel { 
                UserName = admin.UserName,
                NumberOfCandidates = admin.CountCandidacteRegisterAsync,
                NumberOfElections = admin.CountElectionsRegisterAsync,
                NumberOfMatches = admin.PoliticalPartyAsync,
                NumberOfRegisteredCitizens = admin.CountCitizensRegisterAsync
            };
            return View(view);
        }

        public async Task<IActionResult> SelectYear()
        {
            var years = await _availableYearsQuery.AvailableYearAsync();
            if (!years.IsValid)
            {
                foreach(var item in years.errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                    return View("SelectYear");
                }
            }
        
            return View(new AdminSearchDateViewModel { Year = 0, YearAvaible = years.Value!.Select(x => x.YearElection).ToList()});
        }

        [HttpPost]
        public  async Task<IActionResult> SelectYear(AdminSearchDateViewModel model)
        {

            if (!ModelState.IsValid) {
                ModelState.AddModelError("", "Error debe seleccionar un año valido, falloe en el procesamiento de los datos.");
                return RedirectToAction(nameof(Index));
            }
            var validate = await _electionByYearQuery.GetRegisterAsync(new DateTime(model.Year, 1,1));
            if (!validate.IsValid)
            {
                foreach (var item in validate.errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return View(nameof(SelectYear));
            }

            return RedirectToAction(nameof(SummayElection));
        }

        public async Task<IActionResult> SummayElection(int year)
        {

            var list = await _electionByYearQuery.GetRegisterAsync(new DateTime(year, 1, 1));
            var view = new List<ElectoralSummaryViewModel>();
            foreach (var item in list.Value!)
            {

                var e = new ElectoralSummaryViewModel
                {
                    DateRealized = item.DateRealized,
                    NameElection = item.NameElection,
                    NumberCandidactesParticipating = item.NumberCandidactesParticipating,
                    NumberCitizenParticipating = item.NumberCitizenParticipating,
                    NumberParticipatingMatches = item.NumberParticipatingMatches,
                };
                view.Add(e);
            }

            return View(view);
        }

       

    }
}
