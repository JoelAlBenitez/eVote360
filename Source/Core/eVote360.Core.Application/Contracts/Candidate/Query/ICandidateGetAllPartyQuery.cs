using eVote360.Core.Application.DTOs.Candidates;
using System.Collections.Generic;

namespace eVote360.Core.Application.Contracts.Candidate.Query
{
    public interface ICandidateGetAllPartyQuery
    {
        Task<IEnumerable<CandidateDTO>> GetAllPartyAsync(int PartyId);
    }
}
