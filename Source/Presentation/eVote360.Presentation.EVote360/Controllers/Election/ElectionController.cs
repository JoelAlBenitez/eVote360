using eVote360.Core.Application.Contracts.Admin.Query;
using eVote360.Core.Application.Contracts.Election.Commands;
using eVote360.Core.Application.Contracts.Election.Query;
using eVote360.Core.Application.DTOs.Election;
using eVote360.Core.Application.Services.Admin;
using eVote360.Core.Application.ViewModels.Admin;
using eVote360.Core.Application.ViewModels.Election;
using eVote360.Core.Domain.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace eVote360.Presentation.EVote360.Controllers.Election
{
    [Authorize(Roles = "Admin")]
    public class ElectionController : Controller
    {
        private readonly IElectionCreateCommand _createCommand;
        private readonly IElectionUpdateCommand _updateCommand;
        private readonly IElectionAlterStateCommand _alterStateCommand;
        private readonly IElectionActivateCommand _activateCommand;
        private readonly IAvailableYearsQuery _availableYearsQuery;


        private readonly IElectionGetAllQuery _getAllQuery;
        private readonly IElectionGetByIdQuery _getByIdQuery;
        private readonly IElectionByYearQuery _getByYearQuery;
        private readonly IGetElectionReportQuery _reportQuery;

        public ElectionController(
            IElectionCreateCommand createCommand,
            IElectionUpdateCommand updateCommand,
            IElectionAlterStateCommand alterStateCommand,
            IElectionActivateCommand activateCommand,
            IElectionGetAllQuery getAllQuery,
            IElectionGetByIdQuery getByIdQuery,
            IElectionByYearQuery getByYearQuery,
            IGetElectionReportQuery reportQuery,
            IAvailableYearsQuery availableYearsQuery    )
        {
            _createCommand = createCommand;
            _updateCommand = updateCommand;
            _alterStateCommand = alterStateCommand;
            _activateCommand = activateCommand;
            _getAllQuery = getAllQuery;
            _getByIdQuery = getByIdQuery;
            _getByYearQuery = getByYearQuery;
            _reportQuery = reportQuery;
            _availableYearsQuery = availableYearsQuery;

        }

        public async Task<IActionResult> Index()
        {
            var elections = await _getAllQuery.ExecuteAsync();
            var views = new List<ElectionViewModel>();

            foreach (var item in elections)
            {
                views.Add(new ElectionViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    ElectionDate = item.ElectionDate,
                    ElectionState = item.ElectionState,
                    NumberElectivePositionsParticipating = item.NumberCandidactesParticipating,
                    NumberCitizenParticipating = item.NumberCitizenParticipating,
                    NumberParticipatingMatches = item.NumberParticipatingMatches
                });
            }
            return View("~/Views/ElectionManager/Index.cshtml", views);
        }

        [HttpPost]
        public async Task<IActionResult> SelectYear(AdminSearchDateViewModel model)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error debe seleccionar un año valido, fallo en el procesamiento de los datos.");
                return RedirectToAction(nameof(Index));
            }
            var validate = await _getByYearQuery.GetRegisterAsync(new DateTime(model.Year, 1, 1));
            if (!validate.IsValid)
            {
                foreach (var item in validate.errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return View("~/Views/ElectionManager/SelectYear.cshtml", model);
            }

            return RedirectToAction(nameof(SummayElection));
        }

        public async Task<IActionResult> SummayElection(int year)
        {

            var list = await _getByYearQuery.GetRegisterAsync(new DateTime(year, 1, 1));
            var view = new List<ElectionResumsViewModel>();
            foreach (var item in list.Value!)
            {

                var e = new ElectionResumsViewModel
                {
                    PositionName = item.PositionName,
                    CandidateName = item.CandidateName,
                    PartyAcronym = item.PartyAcronym,
                    PartyLogo = item.PartyLogo,
                    PartyName = item.PartyName,
                    Percentage = item.Percentage,
                    ResultStatus = item.ResultStatus,
                    TotalVotes = item.TotalVotes

                };
                view.Add(e);
            }

            return View("~/Views/ElectionManager/SummayElection.cshtml", view); ;
        }

        public async Task<IActionResult> SelectYear()
        {
            var years = await _availableYearsQuery.AvailableYearAsync();
            if (!years.IsValid)
            {
                foreach (var item in years.errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                    return View("~/Views/ElectionManager/SelectYear.cshtml");
                }
            }
            return View("~/Views/ElectionManager/SelectYear.cshtml", new AdminSearchDateViewModel { Year = 0, YearAvaible = years.Value!.Select(x => x.YearElection).ToList() });

        }


        [HttpGet]
        public async Task<IActionResult> GetActive()
        {
            var elections = await _getAllQuery.ExecuteAsync();
            var views = elections
                .Where(e => e.ElectionState == ElectionState.Activa)
                .Select(item => new ElectionViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    ElectionDate = item.ElectionDate,
                    ElectionState = item.ElectionState,
                    NumberCitizenParticipating = item.NumberCitizenParticipating,
                    NumberElectivePositionsParticipating = item.NumberCandidactesParticipating,
                    NumberParticipatingMatches = item.NumberParticipatingMatches
                }).ToList();
            return View("~/Views/ElectionManager/Index.cshtml", views);
        }

        public IActionResult Create()
        {
            return View("~/Views/ElectionManager/Save.cshtml", new ElectionCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ElectionCreateViewModel model)
        {
            if (!ModelState.IsValid) return View("~/Views/ElectionManager/Save.cshtml", model);

            var dto = new ElectionDto
            {
                Name = model.ElectionName!,
                ElectionDate = model.ElectionDate,
                State = true
            };

            var result = await _createCommand.ExecuteAsync(dto);

            if (!result.IsValid)
            {
                foreach (var error in result.errors) { ModelState.AddModelError(error.Code, error.Description); }
                return View("~/Views/ElectionManager/Save.cshtml", model);
            }

            TempData["Message"] = "Elección creada con éxito";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _getByIdQuery.ExecuteAsync(id);

            if (result == null || result.Value == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var election = result.Value;

            var viewModel = new ElectionEditViewModel
            {
                Id = election.Id,
                Name = election.Name,
                ElectionDate = election.ElectionDate,
                ElectionState = election.ElectionState,
                State = election.State
            };

            return View("~/Views/ElectionManager/Edit.cshtml", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ElectionEditViewModel model)
        {
            if (!ModelState.IsValid) return View("~/Views/ElectionManager/Edit.cshtml", model);

            var dto = new ElectionDto
            {
                Id = model.Id,
                Name = model.Name,
                ElectionDate = model.ElectionDate,
                ElectionState = model.ElectionState,
                State = model.State,
                
                
            };
            var result = await _updateCommand.ExecuteAsync(dto);

            if (!result.IsValid)
            {
                foreach (var error in result.errors) { ModelState.AddModelError(error.Code, error.Description); }
                return View("~/Views/ElectionManager/Edit.cshtml", model);
            }

            TempData["TypeAlert"] = "success";
            TempData["Message"] = "Elección actualizada con éxito";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmActivate(int id)
        {
            var result = await _getByIdQuery.ExecuteAsync(id);
            if (result == null || result.Value == null) return RedirectToAction(nameof(Index));
            var election = result.Value;
            if (election.ElectionState != ElectionState.Pendiente)
            {
                TempData["TypeAlert"] = "danger";
                TempData["Message"] = "Solo se pueden activar elecciones en estado pendiente.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ElectionId = id;
            ViewBag.ElectionName = election.Name;
            return View("~/Views/ElectionManager/ConfirmActivate.cshtml");
        }

        [HttpPost]
        [ActionName("ConfirmActivate")]
        public async Task<IActionResult> ConfirmActivatePost(int id)
        {
            var elections = await _getAllQuery.ExecuteAsync();
            bool alreadyActive = elections.Any(e => e.ElectionState == ElectionState.Activa);
            if (alreadyActive)
            {
                TempData["TypeAlert"] = "danger";
                TempData["Message"] = "No se puede activar: ya existe una elección activa.";
                return RedirectToAction(nameof(Index));
            }

            var result = await _activateCommand.ExecuteAsync(id);
            if (!result.IsValid)
            {
                var primerError = result.errors.FirstOrDefault();
                TempData["TypeAlert"] = "danger";
                TempData["Message"] = primerError?.Description ?? "Error al activar la elección.";
            }
            else
            {
                TempData["TypeAlert"] = "success";
                TempData["Message"] = "Elección activada con éxito. Los ciudadanos pueden votar.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmFinalize(int id)
        {
            var result = await _getByIdQuery.ExecuteAsync(id);
            if (result == null || result.Value == null) return RedirectToAction(nameof(Index));
            var election = result.Value;
            if (election.ElectionState != ElectionState.Activa)
            {
                TempData["TypeAlert"] = "danger";
                TempData["Message"] = "Solo se pueden finalizar elecciones activas.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ElectionId = id;
            ViewBag.ElectionName = election.Name;
            return View("~/Views/ElectionManager/ConfirmFinalize.cshtml");
        }

        [HttpPost]
        [ActionName("ConfirmFinalize")]
        public async Task<IActionResult> ConfirmFinalizePost(int id)
        {
            var result = await _alterStateCommand.ExecuteAsync(id);
            if (!result.IsValid)
            {
                var primerError = result.errors.FirstOrDefault();
                TempData["TypeAlert"] = "danger";
                TempData["Message"] = primerError?.Description ?? "Error al finalizar la elección.";
            }
            else
            {
                TempData["TypeAlert"] = "success";
                TempData["Message"] = "Elección finalizada. Los resultados ya están disponibles.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AlterState(int id)
        {
            var result = await _alterStateCommand.ExecuteAsync(id);

            if (!result.IsValid)
            {
                var primerError = result.errors.FirstOrDefault();
                TempData["TypeAlert"] = "danger";
                TempData["Message"] = primerError != null ? primerError.Description : "Error al intentar cambiar el estado.";
            }
            else
            {
                TempData["TypeAlert"] = "success";
                TempData["Message"] = "Estado modificado con éxito.";
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Results(int id)
        {
            var reportResult = await _reportQuery.ExecuteAsync(id);

            if (reportResult == null)
            {
                TempData["Message"] = "Aún no hay resultados procesados para esta elección.";
                return RedirectToAction(nameof(Index));
            }

            var view = reportResult.Select(d => new ElectionResumsViewModel
            {
                PositionName = d.PositionName,
                CandidateName = d.CandidateName,
                PartyAcronym = d.PartyAcronym,
                PartyLogo = d.PartyLogo,
                PartyName = d.PartyName,
                Percentage = d.Percentage,
                ResultStatus = d.ResultStatus,
                TotalVotes = d.TotalVotes,
            }).ToList();
            return View("~/Views/ElectionManager/Results.cshtml", view);
        }
    }
}
