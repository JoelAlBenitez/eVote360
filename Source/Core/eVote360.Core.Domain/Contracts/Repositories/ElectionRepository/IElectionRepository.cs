using eVote360.Core.Domain.Contracts.Repositories.BaseRepository;
using eVote360.Core.Domain.Entities.Election;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Contracts.Repositories.ElectionRepository
{
    public interface IElectionRepository : IBaseRepository<Election, int>
    {
        Task<IEnumerable<Election>> GetAllElectionsAsync();
        Task<Election?> GetActivateElectionAsync();

        Task<IReadOnlyCollection<Election>> GetElectionsByYearAsync(int year);
        Task<bool> DeactivateElectionAsync(int id);
    }
}
