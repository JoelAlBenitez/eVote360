using eVote360.Core.Application.DTOs.Elector.Dashboard;

namespace eVote360.Core.Application.Contracts.Elector.Query
{
    public  interface IWindowElectivePositionQuery
    {
        Task<IReadOnlyCollection<WindowsElectivePositionsDto>> GetWindowsElectivePositionsAsync();
    }
}
