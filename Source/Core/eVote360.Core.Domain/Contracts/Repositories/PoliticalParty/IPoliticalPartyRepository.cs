using eVote360.Core.Domain.Contracts.Repositories.BaseRepository;
using eVote360.Core.Domain.Entities.PoliticalParty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Contracts.Repositories.PoliticalParty
{
    public interface IPoliticalPartyRepository : IBaseRepository<Entities.PoliticalParty.PoliticalParty, int>
    {
        Task<bool> ValidateUniqueAcronymAsync(string acronym);

        Task<bool> ExistByNameAsync (string name);

        Task<IEnumerable<Entities.PoliticalParty.PoliticalParty>> GetActivePartiesAsync();

        Task<bool> HasParticipatedInElectionsAsync(int electionPartyId);

    }
}
