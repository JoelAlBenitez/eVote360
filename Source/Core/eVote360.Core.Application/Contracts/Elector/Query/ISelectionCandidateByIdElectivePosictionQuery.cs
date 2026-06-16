using eVote360.Core.Application.DTOs.Elector.Dashboard;

namespace eVote360.Core.Application.Contracts.Elector.Query
{
    public interface ISelectionCandidateByIdElectivePosictionQuery
    {
        Task<IReadOnlyCollection<SelectionCandidacteDto>> GetSelectionCandidacteAsync(int IdElectivePosition);
    }
}
