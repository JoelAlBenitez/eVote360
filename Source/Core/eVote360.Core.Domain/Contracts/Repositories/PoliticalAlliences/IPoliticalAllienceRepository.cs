
using eVote360.Core.Domain.Contracts.Repositories.BaseRepository;
using eVote360.Core.Domain.Entities.PoliticalAlliances;
namespace eVote360.Core.Domain.Contracts.Repositories.PoliticalAlliences
{
    public interface IPoliticalAllienceRepository : IBaseRepository<PoliticalAlliances, int>
    {

        // metodos de solo lectura xd
        Task<IEnumerable<PoliticalAlliances>> GetPendingReceivedAsync(int partyId);
        Task<IEnumerable<PoliticalAlliances>> GetSentRequestsAsync(int partyId);
        Task<IEnumerable<PoliticalAlliances>> GetActiveAlliancesAsync(int partyId);

        // metodo delete 
        Task<bool> DeleteAsync(int allianceId);

    }
}
