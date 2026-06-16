using eVote360.Core.Application.Contracts.Authentication.Command;
using eVote360.Core.Application.Contracts.Candidate.Commands;
using eVote360.Core.Application.Contracts.Candidate.Query;
using eVote360.Core.Application.DTOs.Candidates;
using eVote360.Core.Application.ViewModels.Candidates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eVote360.Presentation.EVote360.Controllers.Candidates
{
    [Authorize(Roles = "DirigentePolitico")]
    public class CandidateController : Controller
    {
        private readonly ICandidateCreateCommand _candidateCreateCommand;
        private readonly ICandidateUpdateCommand _candidateUpdateCommand;
        private readonly ICandidateChangeStateCommand _candidateChangeStateCommand;
        private readonly ICandidateGetByIdQuery _candidateGetByIdQuery;
        private readonly ICandidateGetAllPartyQuery _candidateGetAllPartyQuery;
        private readonly ISessionUser _sessionUser;

        private int GetPartyId() => _sessionUser.GetPoliticalParty();
        private int GetUserId() => _sessionUser.GetUserId();

        public CandidateController(
            ICandidateCreateCommand candidateCreateCommand,
            ICandidateUpdateCommand candidateUpdateCommand,
            ICandidateChangeStateCommand candidateChangeStateCommand,
            ICandidateGetByIdQuery candidateGetByIdQuery,
            ICandidateGetAllPartyQuery candidateGetAllPartyQuery,
            ISessionUser sessionUser)
        {
            _candidateCreateCommand = candidateCreateCommand;
            _candidateUpdateCommand = candidateUpdateCommand;
            _candidateChangeStateCommand = candidateChangeStateCommand;
            _candidateGetByIdQuery = candidateGetByIdQuery;
            _candidateGetAllPartyQuery = candidateGetAllPartyQuery;
            _sessionUser = sessionUser;
        }

        public async Task<IActionResult> Index()
        {
            var candidates = await _candidateGetAllPartyQuery.GetAllPartyAsync(GetPartyId());
            var candidateViewModels = new List<CandidateViewModel>();

            if (candidates != null)
            {
                foreach (var item in candidates)
                {
                    var candidateVm = new CandidateViewModel()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        LastName = item.LastName,
                        State = item.State,
                        PhotoUrl = item.PhotoUrl,
                        PoliticalPartyId = item.PoliticalPartyId,
                        HasParticipatedInElection = item.HasParticipatedInElection
                    };
                    candidateViewModels.Add(candidateVm);
                }
            }

            return View(candidateViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> GetCandidate(int Id)
        {
            var result = await _candidateGetByIdQuery.GetByIdAsync(Id, GetPartyId());
            
            if (!result.IsValid)
            {
                foreach (var item in result.errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return RedirectToAction(nameof(Index));
            }

            var vm = new CandidateViewModel
            {
                Id = result.Value!.Id,
                Name = result.Value.Name,
                LastName = result.Value.LastName,
                State = result.Value.State,
                PhotoUrl = result.Value.PhotoUrl,
                PoliticalPartyId = result.Value.PoliticalPartyId,
                HasParticipatedInElection = result.Value.HasParticipatedInElection
            };

            return PartialView("_ViewCandidate", vm);
        }

        public Task<IActionResult> Create()
        {
            return Task.FromResult<IActionResult>(View("Save", new CandidateViewModelCreate
            {
                Name = "",
                LastName = ""
            }));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CandidateViewModelCreate candidateVm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error en la creación y carga del formulario");
                return View("Save", candidateVm); 
            }

            var dto = new CreateCandidateDto
            {
                Name = candidateVm.Name,
                LastName = candidateVm.LastName,
                PhotoUrl = candidateVm.PhotoFile
            };

            var createResult = await _candidateCreateCommand.CreateCandidateAsync(dto, GetPartyId());

            if (!createResult.IsValid)
            {
                foreach (var item in createResult.errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return View("Save", candidateVm);
            }

            TempData["Message"] = "Candidato creado exitosamente";
            TempData["TypeAlert"] = "success";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var result = await _candidateGetByIdQuery.GetByIdAsync(Id, GetPartyId());
            
            if (!result.IsValid)
            {
                foreach (var item in result.errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return RedirectToAction(nameof(Index));
            }

            var candidateVm = new CandidateViewModelEdit
            {
                Id = result.Value!.Id,
                Name = result.Value.Name,
                LastName = result.Value.LastName,
                CurrentPhotoUrl = result.Value.PhotoUrl,
                HasParticipatedInElection = result.Value.HasParticipatedInElection
            };

            return View("Edit", candidateVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CandidateViewModelEdit candidateVm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error en la edición y carga del formulario");
                return View("Edit", candidateVm);
            }

            var dto = new UpdateCandidateDto
            {
                Id = candidateVm.Id,
                Name = candidateVm.Name,
                LastName = candidateVm.LastName,
                PhotoUrl = candidateVm.PhotoFile
            };

            var editResult = await _candidateUpdateCommand.UpdateCandidateAsync(dto, GetPartyId());

            if (!editResult.IsValid)
            {
                foreach (var item in editResult.errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return View("Edit", candidateVm);
            }

            TempData["Message"] = "Candidato editado exitosamente";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AlterState(int Id)
        {
            var result = await _candidateGetByIdQuery.GetByIdAsync(Id, GetPartyId());
            
            if (!result.IsValid)
            {
                foreach (var item in result.errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return RedirectToAction(nameof(Index));
            }

            var candidateVm = new CandidateViewModelAlterState
            {
                Id = result.Value!.Id,
                FullName = $"{result.Value.Name} {result.Value.LastName}"
            };

            return View("AlterState", candidateVm);
        }

        [HttpPost]
        public async Task<IActionResult> AlterState(CandidateViewModelAlterState candidateVm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error en la selección del registro y carga del elemento");
                return RedirectToAction(nameof(Index));
            }

            var alterResult = await _candidateChangeStateCommand.ChangeStateAsync(candidateVm.Id, GetPartyId());

            if (!alterResult.IsValid)
            {
                foreach (var item in alterResult.errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return View("AlterState", candidateVm); 
            }

            TempData["Message"] = "Se modificó el estado del candidato exitosamente";
            return RedirectToAction(nameof(Index));
        }
    }
}
