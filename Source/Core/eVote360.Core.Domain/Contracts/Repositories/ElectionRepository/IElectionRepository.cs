using eVote360.Core.Domain.Contracts.Repositories.BaseRepository;
using eVote360.Core.Domain.Entities.Election;



namespace eVote360.Core.Domain.Contracts.Repositories.ElectionRepository
{
    public interface IElectionRepository : IBaseRepository<Election, int>
    {
        Task<IEnumerable<Election>> GetAllElectionsAsync();
        Task<Election?> GetActivateElectionAsync();

        Task<IReadOnlyCollection<Election>> GetElectionsByYearAsync(int year);
        Task<bool> DeactivateElectionAsync(int id);
        Task<bool> ActivateElectionAsync(int id);
        Task<IReadOnlyCollection<ElectionResult>> GetElectionResultAsync (int electionId);
    }
}
