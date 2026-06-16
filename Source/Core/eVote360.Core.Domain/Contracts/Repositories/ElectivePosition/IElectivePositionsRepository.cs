using eVote360.Core.Domain.Contracts.Repositories.BaseRepository;
using eVote360.Core.Domain.Entities.ElectivePosition;

namespace eVote360.Core.Domain.Contracts.Repositories.ElectivePosition
{
    public interface IElectivePositionsRepository : IBaseRepository<ElectivePositions, int>
    {
        Task<IReadOnlyCollection<ElectivePositions>> GetAllAsync();
        Task<IReadOnlyCollection<ElectivePositions>> GetAllActiveAsync();
        Task<IReadOnlyCollection<ElectivePositions>> GetAllDateAsync(DateTimeOffset? dateStart, DateTimeOffset? dateEnd);
    }
}
