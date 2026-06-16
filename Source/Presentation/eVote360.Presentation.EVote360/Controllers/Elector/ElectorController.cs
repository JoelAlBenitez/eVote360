using eVote360.Core.Application.Contracts.Elector.Commands.Code;
using eVote360.Core.Application.Contracts.Elector.Commands.ElectorSession;
using eVote360.Core.Application.Contracts.Elector.Commands.Identification;
using eVote360.Core.Application.Contracts.Elector.Commands.Votes;
using eVote360.Core.Application.Contracts.Elector.Query;
using eVote360.Core.Application.DTOs.Elector.Code;
using eVote360.Core.Application.DTOs.Elector.Identification;
using eVote360.Core.Application.DTOs.Elector.Select;
using eVote360.Core.Application.ViewModels.Elector.Code;
using eVote360.Core.Application.ViewModels.Elector.Dashboard;
using eVote360.Core.Application.ViewModels.Elector.Identification;
using Microsoft.AspNetCore.Mvc;

namespace eVote360.Presentation.EVote360.Controllers.Elector
{
    public class ElectorController : Controller
    {
        private readonly IIdentificationVerifyCommand _identificationVerifyCommand;
        private readonly IIdentificationVerifyCompareIdentificationByImg _identificationVerifyCompareByImg;
        private readonly ICodeVerificationCommand _codeVerificationCommand;
        private readonly IWindowElectivePositionQuery _windowElectivePositionQuery;
        private readonly ISelectionCandidateByIdElectivePosictionQuery _selectionCandidateQuery;
        private readonly IElectorSession _electorSession;
        private readonly IProcessVoteElectorCommand _processVoteElectorCommand;
        private readonly IOcrVerificationCommand _ocrVerificationCommand;

        public ElectorController(
            IIdentificationVerifyCommand identificationVerifyCommand,
            IIdentificationVerifyCompareIdentificationByImg identificationVerifyCompareByImg,
            ICodeVerificationCommand codeVerificationCommand,
            IWindowElectivePositionQuery windowElectivePositionQuery,
            ISelectionCandidateByIdElectivePosictionQuery selectionCandidateQuery,
            IElectorSession electorSession,
            IProcessVoteElectorCommand processVoteElectorCommand,
            IOcrVerificationCommand ocrVerificationCommand


            )
        {
            _identificationVerifyCommand = identificationVerifyCommand;
            _identificationVerifyCompareByImg = identificationVerifyCompareByImg;
            _codeVerificationCommand = codeVerificationCommand;
            _windowElectivePositionQuery = windowElectivePositionQuery;
            _selectionCandidateQuery = selectionCandidateQuery;
            _electorSession = electorSession;
            _processVoteElectorCommand = processVoteElectorCommand;
            _ocrVerificationCommand = ocrVerificationCommand;
        }

        public IActionResult IdentifyElector()
        {
            return View(new IdentificationViewModel { Identification = string.Empty });
        }

        [HttpPost]
        public async Task<IActionResult> IdentifyElector(IdentificationViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _identificationVerifyCommand.VerifyIdentificationByCitizen(
                new IdentificiationDto(model.Identification));

            if (!result.IsValid)
            {
                foreach (var error in result.errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return View(model);
            }

            _electorSession.SetIdentification(model.Identification);
            return RedirectToAction(nameof(UploadIdentification));
        }

        public IActionResult UploadIdentification()
        {
            return View(new IdentifiationFileImagenViewModel { IdentificationImg = null! });
        }

        [HttpPost]
        public async Task<IActionResult> UploadIdentification(IdentifiationFileImagenViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            using var memoryStream = new MemoryStream();
            await model.IdentificationImg.CopyToAsync(memoryStream);
            var imageBytes = memoryStream.ToArray();

            var result = await _ocrVerificationCommand.VerifyOcrAndCreateCodeAsync(
                new OcrVerificationDto(imageBytes));

            if (!result.IsValid)
            {
                foreach (var error in result.errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return View(model);
            }

            return RedirectToAction(nameof(VerifyCode));
        }

        public IActionResult VerifyCode()
        {
            if (string.IsNullOrEmpty(_electorSession.GetIdentification()))
                return RedirectToAction(nameof(IdentifyElector));

            return View(new CodeVerificationViewModel { CodeVerification = 0 });
        }

        [HttpPost]
        public async Task<IActionResult> VerifyCode(CodeVerificationViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _codeVerificationCommand.VerifyCodeVerification(
                new CodeVerificationDto(model.CodeVerification));

            if (!result.IsValid)
            {
                foreach (var error in result.errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return View(model);
            }

            return RedirectToAction(nameof(Dashboard));
        }

        public async Task<IActionResult> Dashboard()
        {
            if (string.IsNullOrEmpty(_electorSession.GetIdentification()))
                return RedirectToAction(nameof(IdentifyElector));

            var positions = await _windowElectivePositionQuery.GetWindowsElectivePositionsAsync();
            var currentSelections = _electorSession.GetCurrentSelections();

            var viewModel = positions.Select(p => new ElectorElectionPositionViewModel
            {
                IdElectivePosition = p.IdElectivePosition.ToString(),
                NameElectivePosition = p.NameElectivePosition,
                NumberPoliticalParty = p.NumberPoliticalParty,
                NumberOfActualCandidactes = p.NumberActualCandidactes,
                StateSelection = currentSelections.ContainsKey(p.IdElectivePosition)
            }).ToList();

            return View(viewModel);
        }

        public async Task<IActionResult> SelectPosition(int id)
        {
            if (string.IsNullOrEmpty(_electorSession.GetIdentification()))
                return RedirectToAction(nameof(IdentifyElector));

            var candidates = await _selectionCandidateQuery.GetSelectionCandidacteAsync(id);

            var viewModel = candidates.Select(c => new ElectorCandidactesViewModel
            {
                IdCandidacte = c.IdCandidacte,
                PhotoUrlCandidacte = c.PhotoCandidacte,
                NameCandidacte = c.NameCandidacte,
                PoliticalParty = c.PoliticalParty,
                LogoPoliticalParty = c.PoliticalPartyLogoUrl
            }).ToList();

            ViewBag.IdElectivePosition = id;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveSelection(int idElectivePosition, int? idCandidate, bool noApply)
        {
            _electorSession.SaveSelection(idElectivePosition, idCandidate, noApply);
            return RedirectToAction(nameof(Dashboard));
        }

        public IActionResult ConfirmVote()
        {
            if (string.IsNullOrEmpty(_electorSession.GetIdentification()))
                return RedirectToAction(nameof(IdentifyElector));

            var selections = _electorSession.GetCurrentSelections();
            return View(selections.Values.ToList());
        }

        [HttpPost]
        [ActionName("ConfirmVote")]
        public async Task<IActionResult> ConfirmVotePost()
        {
            var selections = _electorSession.GetCurrentSelections();

            var selectedVotes = selections.Values.Select(s => new SelectedVoteDto
            {
                IdElectivePosition = s.IdPosictionElective,
                IdCandidate = s.IdCandidacteSelection,
                IsNoApply = s.NoApplyCandidacte
            }).ToList();

            var result = await _processVoteElectorCommand.ProcessVoteAsync(selectedVotes);

            if (!result.IsValid)
            {
                foreach (var error in result.errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return View(selections.Values.ToList());
            }

            return RedirectToAction(nameof(VoteSuccess));
        }

        public IActionResult VoteSuccess()
        {
            return View();
        }
    }
}
   

     
