using eVote360.Core.Application.Alliances.DTOs;
using eVote360.Core.Application.Contracts.Alliance.Commands;
using eVote360.Core.Application.Contracts.Alliance.Query;
using eVote360.Core.Application.Contracts.Authentication.Command;
using eVote360.Core.Application.ViewModels.PoliticalAlliances;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVote360.Presentation.EVote360.Controllers.PoliticalAlliances
{
    [Authorize(Roles = "DirigentePolitico")]
    public class PoliticalAlliancesController : Controller
    {
        private readonly ICreateAllianceCommand _createAllianceCommand;
        private readonly IAcceptAllianceCommand _acceptAllianceCommand;
        private readonly IRejectAllianceCommand _rejectAllianceCommand;
        private readonly IDeleteAllianceRequestCommand _deleteAllianceRequestCommand;
        private readonly IDeleteActiveAllianceCommand _deleteActiveAllianceCommand;
        private readonly IGetPendingReceivedAlliancesQuery _getPendingReceivedQuery;
        private readonly IGetSentAllianceRequestsQuery _getSentRequestsQuery;
        private readonly IGetActiveAlliancesQuery _getActiveAlliancesQuery;
        private readonly IGetAllianceByIdQuery _getAllianceByIdQuery;
        private readonly ISessionUser _sessionUser;

        public PoliticalAlliancesController(
            ICreateAllianceCommand createAllianceCommand,
            IAcceptAllianceCommand acceptAllianceCommand,
            IRejectAllianceCommand rejectAllianceCommand,
            IDeleteAllianceRequestCommand deleteAllianceRequestCommand,
            IDeleteActiveAllianceCommand deleteActiveAllianceCommand,
            IGetPendingReceivedAlliancesQuery getPendingReceivedQuery,
            IGetSentAllianceRequestsQuery getSentRequestsQuery,
            IGetActiveAlliancesQuery getActiveAlliancesQuery,
            IGetAllianceByIdQuery getAllianceByIdQuery,
            ISessionUser sessionUser)
        {
            _createAllianceCommand = createAllianceCommand;
            _acceptAllianceCommand = acceptAllianceCommand;
            _rejectAllianceCommand = rejectAllianceCommand;
            _deleteAllianceRequestCommand = deleteAllianceRequestCommand;
            _deleteActiveAllianceCommand = deleteActiveAllianceCommand;
            _getPendingReceivedQuery = getPendingReceivedQuery;
            _getSentRequestsQuery = getSentRequestsQuery;
            _getActiveAlliancesQuery = getActiveAlliancesQuery;
            _getAllianceByIdQuery = getAllianceByIdQuery;
            _sessionUser = sessionUser;
        }

        private int GetAuthenticatedPartyId() => _sessionUser.GetPoliticalParty(); 
        private int GetAuthenticatedUserId() => _sessionUser.GetUserId();

        public async Task<IActionResult> Index()
        {
            int partyId = GetAuthenticatedPartyId();
            var indexVm = new AllianceIndexViewModel();

            // 1. Obtener solicitudes pendientes recibidas
            var pendingResult = await _getPendingReceivedQuery.ExecuteAsync(partyId);
            if (pendingResult.IsValid)
            {
                indexVm.PendingReceived = pendingResult.Value!.Select(d => new AllianceViewModel
                {
                    Id = d.Id,
                    RequestingPartyId = d.RequestingPartyId,
                    ReceivingPartyId = d.ReceivingPartyId,
                    PartyName = d.RequestingPartyName ?? "N/A",
                    PartySiglas = d.RequestingPartySiglas ?? "N/A",
                    Status = d.Status.ToString(),
                    RequestDate = d.RequestDate
                }).ToList();
            }
            else
            {
                foreach (var err in pendingResult.errors) ModelState.AddModelError(err.Code, err.Description);
            }

            // 2. Obtener solicitudes enviadas
            var sentResult = await _getSentRequestsQuery.ExecuteAsync(partyId);
            if (sentResult.IsValid)
            {
                indexVm.SentRequests = sentResult.Value!.Select(d => new AllianceViewModel
                {
                    Id = d.Id,
                    RequestingPartyId = d.RequestingPartyId,
                    ReceivingPartyId = d.ReceivingPartyId,
                    PartyName = d.ReceivingPartyName ?? "N/A",
                    PartySiglas = d.ReceivingPartySiglas ?? "N/A",
                    Status = d.Status.ToString(),
                    RequestDate = d.RequestDate
                }).ToList();
            }
            else
            {
                foreach (var err in sentResult.errors) ModelState.AddModelError(err.Code, err.Description);
            }

            // 3. Obtener alianzas activas
            var activeResult = await _getActiveAlliancesQuery.ExecuteAsync(partyId);
            if (activeResult.IsValid)
            {
                indexVm.ActiveAlliances = activeResult.Value!.Select(d => new AllianceViewModel
                {
                    Id = d.Id,
                    RequestingPartyId = d.RequestingPartyId,
                    ReceivingPartyId = d.ReceivingPartyId,
                    // El partido contrario es el que NO es el nuestro
                    PartyName = (d.RequestingPartyId == partyId ? d.ReceivingPartyName : d.RequestingPartyName) ?? "N/A",
                    PartySiglas = (d.RequestingPartyId == partyId ? d.ReceivingPartySiglas : d.RequestingPartySiglas) ?? "N/A",
                    Status = d.Status.ToString(),
                    RequestDate = d.RequestDate,
                    ResponseDate = d.AcceptedDate
                }).ToList();
            }
            else
            {
                foreach (var err in activeResult.errors) ModelState.AddModelError(err.Code, err.Description);
            }

            // TODO: Integrar con servicio de elecciones para indexVm.HasActiveElection
            indexVm.HasActiveElection = false; 

            return View(indexVm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new CreateAllianceViewModel();
            // TODO: Poblar AvailableParties cuando exista el módulo de partidos
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAllianceViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var dto = new CreateAllianceRequestDto { ReceivingPartyId = vm.ReceivingPartyId };
            var result = await _createAllianceCommand.ExecuteAsync(dto, GetAuthenticatedPartyId(), GetAuthenticatedUserId());

            if (!result.IsValid)
            {
                foreach (var err in result.errors) ModelState.AddModelError(err.Code, err.Description);
                return View(vm);
            }

            TempData["Message"] = "Solicitud de alianza creada exitosamente";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Accept(int Id)
        {
            var result = await _getAllianceByIdQuery.ExecuteAsync(Id);
            if (!result.IsValid)
            {
                foreach (var err in result.errors) ModelState.AddModelError(err.Code, err.Description);
                return RedirectToAction(nameof(Index));
            }

            var vm = new AllianceConfirmationViewModel
            {
                AllianceId = result.Value!.Id,
                PartyName = result.Value.RequestingPartyName ?? "N/A",
                PartySiglas = result.Value.RequestingPartySiglas ?? "N/A"
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Accept(AllianceConfirmationViewModel vm)
        {
            var result = await _acceptAllianceCommand.ExecuteAsync(vm.AllianceId, GetAuthenticatedPartyId());

            if (!result.IsValid)
            {
                foreach (var err in result.errors) ModelState.AddModelError(err.Code, err.Description);
                return RedirectToAction(nameof(Index));
            }

            TempData["Message"] = "Alianza aceptada exitosamente";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Reject(int Id)
        {
            var result = await _getAllianceByIdQuery.ExecuteAsync(Id);
            if (!result.IsValid)
            {
                foreach (var err in result.errors) ModelState.AddModelError(err.Code, err.Description);
                return RedirectToAction(nameof(Index));
            }

            var vm = new AllianceConfirmationViewModel
            {
                AllianceId = result.Value!.Id,
                PartyName = result.Value.RequestingPartyName ?? "N/A",
                PartySiglas = result.Value.RequestingPartySiglas ?? "N/A"
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Reject(AllianceConfirmationViewModel vm)
        {
            var result = await _rejectAllianceCommand.ExecuteAsync(vm.AllianceId, GetAuthenticatedPartyId());

            if (!result.IsValid)
            {
                foreach (var err in result.errors) ModelState.AddModelError(err.Code, err.Description);
                return RedirectToAction(nameof(Index));
            }

            TempData["Message"] = "Solicitud rechazada exitosamente";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRequest(int Id)
        {
            var result = await _getAllianceByIdQuery.ExecuteAsync(Id);
            if (!result.IsValid)
            {
                foreach (var err in result.errors) ModelState.AddModelError(err.Code, err.Description);
                return RedirectToAction(nameof(Index));
            }

            var vm = new AllianceConfirmationViewModel
            {
                AllianceId = result.Value!.Id,
                PartyName = result.Value.ReceivingPartyName ?? "N/A",
                PartySiglas = result.Value.ReceivingPartySiglas ?? "N/A"
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRequest(AllianceConfirmationViewModel vm)
        {
            var result = await _deleteAllianceRequestCommand.ExecuteAsync(vm.AllianceId, GetAuthenticatedPartyId());

            if (!result.IsValid)
            {
                foreach (var err in result.errors) ModelState.AddModelError(err.Code, err.Description);
                return RedirectToAction(nameof(Index));
            }

            TempData["Message"] = "Solicitud eliminada exitosamente";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAlliance(int Id)
        {
            var result = await _getAllianceByIdQuery.ExecuteAsync(Id);
            if (!result.IsValid)
            {
                foreach (var err in result.errors) ModelState.AddModelError(err.Code, err.Description);
                return RedirectToAction(nameof(Index));
            }

            int myPartyId = GetAuthenticatedPartyId();
            var vm = new AllianceConfirmationViewModel
            {
                AllianceId = result.Value!.Id,
                PartyName = (result.Value.RequestingPartyId == myPartyId ? result.Value.ReceivingPartyName : result.Value.RequestingPartyName) ?? "N/A",
                PartySiglas = (result.Value.RequestingPartyId == myPartyId ? result.Value.ReceivingPartySiglas : result.Value.RequestingPartySiglas) ?? "N/A"
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAlliance(AllianceConfirmationViewModel vm)
        {
            var result = await _deleteActiveAllianceCommand.ExecuteAsync(vm.AllianceId, GetAuthenticatedPartyId());

            if (!result.IsValid)
            {
                foreach (var err in result.errors) ModelState.AddModelError(err.Code, err.Description);
                return RedirectToAction(nameof(Index));
            }

            TempData["Message"] = "Alianza eliminada exitosamente";
            TempData["TypeAlert"] = "success";
            return RedirectToAction(nameof(Index));
        }
    }
}
