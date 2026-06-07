using eVote360.Core.Domain.Entities.PoliticalParty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Interfaces.Repositories
{
    public interface IPoliticalPartyRepository
    {
        Task<PoliticalParty> GetByIdAsync(int id);
        Task<IEnumerable<PoliticalParty>> GetAllAsync();

        Task AddAsync(PoliticalParty politicalParty);

        Task UpdateAsync(PoliticalParty politicalParty);


    }
}
