
using eVote360.Core.Application.DTOs.ElectivePositions;
namespace eVote360.Core.Application.Contracts.ElectivePosictions.QueryServices
{
    public interface IElectivePosictionsGetActiveQuery
    {
        Task<IReadOnlyCollection<ElectivePosictionsDto>> GetAllActiveElectivePosictionsAsync();

    }
}
