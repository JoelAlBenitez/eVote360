using eVote360.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Interfaces.Repositories
{
    public interface IElectionRepository
    {
        Task<Election> GetElectionByIdAsync(int electionId);

        Task<IEnumerable<Election>> GetAllElectionsAsync();

        Task AddAsync (Election election);

        Task UpdateAsync (Election election);

        Task<Election?> GetActivateElectionAsync();
    }
}
