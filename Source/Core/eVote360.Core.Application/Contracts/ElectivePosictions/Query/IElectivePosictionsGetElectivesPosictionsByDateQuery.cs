using eVote360.Core.Application.DTOs.ElectivePositions;

namespace eVote360.Core.Application.Contracts.ElectivePosictions.Query
{
    public interface IElectivePosictionsGetElectivesPosictionsByDateQuery
    {
        Task<IReadOnlyCollection<ElectivePosictionsDto>> GetElectivePosictionsByDate(DateTimeOffset dateStart, DateTimeOffset dateEnd);

    }
}
