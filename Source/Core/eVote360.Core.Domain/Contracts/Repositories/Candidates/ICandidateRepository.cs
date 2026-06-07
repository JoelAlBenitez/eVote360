using eVote360.Core.Domain.Contracts.Repositories.BaseRepository;
using eVote360.Core.Domain.Entities.Candidate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Contracts.Repositories.Candidate
{
    public interface ICandidateRepository : IBaseRepository<Candidates, int>
    {
        Task<IEnumerable<Candidates>> GetAllByPartyIdAsync(int partyId);
    }
}
