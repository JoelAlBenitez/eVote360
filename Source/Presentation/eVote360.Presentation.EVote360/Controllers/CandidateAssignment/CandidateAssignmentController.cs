using eVote360.Core.Application.CandidateAssignment.DTOs;
using eVote360.Core.Application.Contracts.Authentication.Command;
using eVote360.Core.Application.Contracts.CandidateAssignment.Commands;
using eVote360.Core.Application.Contracts.CandidateAssignment.Query;
using eVote360.Core.Application.ViewModels.CandidateAssignment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVote360.Presentation.EVote360.Controllers.CandidateAssignment
{
    [Authorize(Roles = "DirigentePolitico")]
    public class CandidateAssignmentController : Controller
    {
        private readonly IGetAssignmentsByPartyQuery _getAssignmentsByPartyQuery;
        private readonly IGetAssignmentByIdQuery _getAssignmentByIdQuery;
        private readonly IGetEligibleCandidatesForPositionQuery _getEligibleCandidatesForPositionQuery;
        private readonly ICreateAssignmentCommand _createAssignmentCommand;
        private readonly IDeleteAssignmentCommand _deleteAssignmentCommand;
        private readonly ISessionUser _sessionUser;

        public CandidateAssignmentController(
            IGetAssignmentsByPartyQuery getAssignmentsByPartyQuery,
            IGetAssignmentByIdQuery getAssignmentByIdQuery,
            IGetEligibleCandidatesForPositionQuery getEligibleCandidatesForPositionQuery,
            ICreateAssignmentCommand createAssignmentCommand,
            IDeleteAssignmentCommand deleteAssignmentCommand,
            ISessionUser sessionUser)
        {
            _getAssignmentsByPartyQuery = getAssignmentsByPartyQuery;
            _getAssignmentByIdQuery = getAssignmentByIdQuery;
            _getEligibleCandidatesForPositionQuery = getEligibleCandidatesForPositionQuery;
            _createAssignmentCommand = createAssignmentCommand;
            _deleteAssignmentCommand = deleteAssignmentCommand;
            _sessionUser = sessionUser;
        }

        private int GetPartyId() => _sessionUser.GetPoliticalParty();
        private int GetUserId() => _sessionUser.GetUserId();

        public async Task<IActionResult> Index()
        {
            var result = await _getAssignmentsByPartyQuery.ExecuteAsync(GetPartyId());
            
            if (!result.IsValid)
            {
                foreach (var err in result.errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(new List<CandidateAssignmentViewModel>());
            }

            var viewModels = result.Value!.Select(item => new CandidateAssignmentViewModel
            {
                AssignmentId = item.AssignmentId,
                ElectivePositionId = item.ElectivePositionId,
                ElectivePositionName = item.ElectivePositionName,
                CandidateId = item.CandidateId,
                CandidateName = item.CandidateName,
                CandidateLastName = item.CandidateLastName,
                PhotoUrl = item.PhotoUrl,
                CandidateType = item.CandidateType,
                OriginPartyName = item.OriginPartyName,
                OriginPartySiglas = item.OriginPartySiglas,
                HasCandidate = item.AssignmentId != null && item.AssignmentId != 0
            }).ToList();

            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int electivePositionId, string electivePositionName)
        {
            var result = await _getEligibleCandidatesForPositionQuery.ExecuteAsync(GetPartyId(), electivePositionId);
            
            var vm = new CreateAssignmentViewModel
            {
                ElectivePositionId = electivePositionId,
                ElectivePositionName = electivePositionName
            };

            if (result.IsValid)
            {
                vm.EligibleCandidates = result.Value!.Select(c => new SelectListItem
                {
                    Value = c.CandidateId.ToString(),
                    Text = $"{c.CandidateName} {c.CandidateLastName}"
                }).ToList();
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAssignmentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var resultCandidates = await _getEligibleCandidatesForPositionQuery.ExecuteAsync(GetPartyId(), vm.ElectivePositionId);
                if (resultCandidates.IsValid)
                {
                    vm.EligibleCandidates = resultCandidates.Value!.Select(c => new SelectListItem
                    {
                        Value = c.CandidateId.ToString(),
                        Text = $"{c.CandidateName} {c.CandidateLastName}"
                    }).ToList();
                }
                return View(vm);
            }

            var dto = new CreateAssignmentRequestDto
            {
                CandidateId = vm.CandidateId,
                ElectivePositionId = vm.ElectivePositionId
            };

            var result = await _createAssignmentCommand.ExecuteAsync(dto, GetPartyId(), GetUserId());

            if (!result.IsValid)
            {
                foreach (var err in result.errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }

                var resultCandidates = await _getEligibleCandidatesForPositionQuery.ExecuteAsync(GetPartyId(), vm.ElectivePositionId);
                if (resultCandidates.IsValid)
                {
                    vm.EligibleCandidates = resultCandidates.Value!.Select(c => new SelectListItem
                    {
                        Value = c.CandidateId.ToString(),
                        Text = $"{c.CandidateName} {c.CandidateLastName}"
                    }).ToList();
                }
                return View(vm);
            }

            TempData["Message"] = "Candidato asignado exitosamente";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAssignment(int assignmentId)
        {
            var result = await _getAssignmentByIdQuery.ExecuteAsync(assignmentId);
            
            if (!result.IsValid)
            {
                foreach (var err in result.errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return RedirectToAction(nameof(Index));
            }

            var vm = new DeleteAssignmentViewModel
            {
                AssignmentId = result.Value!.AssignmentId ?? 0,
                CandidateName = $"{result.Value.CandidateName} {result.Value.CandidateLastName}",
                ElectivePositionName = result.Value.ElectivePositionName
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAssignment(DeleteAssignmentViewModel vm)
        {
            var result = await _deleteAssignmentCommand.ExecuteAsync(vm.AssignmentId, GetPartyId());

            if (!result.IsValid)
            {
                foreach (var err in result.errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return RedirectToAction(nameof(Index));
            }

            TempData["Message"] = "Asignación eliminada exitosamente";
            return RedirectToAction(nameof(Index));
        }
    }
}
