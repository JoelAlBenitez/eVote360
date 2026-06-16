using eVote360.Core.Application.Contracts.Elector.Commands.ElectorSession;
using eVote360.Core.Application.ViewModels.Elector.Dashboard;
using System.Text.Json;

namespace eVote360.Presentation.EVote360.Middleware.Elector
{
    public class ElectorSessionService : IElectorSession
    {
        private const string IdentificationKey = "elector_identification";
        private const string SelectionsKey = "elector_selections";
        private const string OCRValidatedKey = "elector_ocr_validated";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        public ElectorSessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetIdentification() =>
            Session.GetString(IdentificationKey) ?? string.Empty;

        public void SetIdentification(string identification) =>
            Session.SetString(IdentificationKey, identification);

        public bool GetValidateOCR() =>
            Session.GetInt32(OCRValidatedKey) == 1;

        public void SetValidateOCR(bool validate) =>
            Session.SetInt32(OCRValidatedKey, validate ? 1 : 0);

        public void SaveSelection(SelectionCandidacteByPositionElectiveViewModel selection)
        {
            var selections = GetCurrentSelections();
            selections[selection.IdPosictionElective] = selection;
            Session.SetString(SelectionsKey, JsonSerializer.Serialize(selections));
        }

        public Dictionary<int, SelectionCandidacteByPositionElectiveViewModel> GetCurrentSelections()
        {
            var json = Session.GetString(SelectionsKey);
            if (string.IsNullOrEmpty(json))
                return new Dictionary<int, SelectionCandidacteByPositionElectiveViewModel>();

            return JsonSerializer.Deserialize<Dictionary<int, SelectionCandidacteByPositionElectiveViewModel>>(json)
                   ?? new Dictionary<int, SelectionCandidacteByPositionElectiveViewModel>();
        }

        public void Clear()
        {
            Session.Remove(IdentificationKey);
            Session.Remove(SelectionsKey);
        }
    }
}
