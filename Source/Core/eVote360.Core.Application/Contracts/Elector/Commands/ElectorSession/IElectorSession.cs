using eVote360.Core.Application.ViewModels.Elector.Dashboard;

namespace eVote360.Core.Application.Contracts.Elector.Commands.ElectorSession
{
    public interface IElectorSession
    {
        string GetIdentification();
        void SetIdentification(string identification);
        void SaveSelection(SelectionCandidacteByPositionElectiveViewModel selection);
        Dictionary<int, SelectionCandidacteByPositionElectiveViewModel> GetCurrentSelections();
        void Clear();
        bool GetValidateOCR();
        void SetValidateOCR(bool validate);
    }
}
