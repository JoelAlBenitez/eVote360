using eVote360.Core.Domain.Entities.Elector.ElectorData;

namespace eVote360.Core.Domain.Contracts.Repositories.Elector.SelectPorcess
{
    public interface ISelectDataForElectoralProcessRepository
    {
        Task<IReadOnlyCollection<ElectorDataElectionPosition>> GetElectorDataElectionPositionsAsync();
        Task<IReadOnlyCollection<ElectorSelectCandidacteElectivepPosiction>> GetElectorSelectCandidacteElectivepPosictionsAsync(int IdElectivePosition);
    }
}
