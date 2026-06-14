using eVote360.Core.Application.Contracts.Admin.Query;
using eVote360.Core.Application.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;

namespace eVote360.Presentation.EVote360.Controllers.Admin
{
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
            var count = await _countRegisterAdminQuery.CountRegisterQueryAsync();

            var view = new AdminViewModel { 
                    NameElection = "",
                    DateRealized = DateTime.Now,
                    NumberCandidactesParticipating = 0,
                    NumberCitizenParticipating = count.CountCitizensRegisterAsync,
                    NumberParticipatingMatches = count.PoliticalPartyAsync,
                    YearAvaible = null!,
            };

            return View( view );
        }
    }
}
