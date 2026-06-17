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


    }
}
