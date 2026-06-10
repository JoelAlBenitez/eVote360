using eVote360.Core.Application.Contracts.Candidate.Query;
using eVote360.Core.Application.DTOs.Candidates;
using eVote360.Core.Domain.Contracts.Repositories.Candidate;
using System.Collections.Generic;

namespace eVote360.Core.Application.Services.Candidate.Query
{
    public class CandidateGetAllParty : ICandidateGetAllPartyQuery
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateGetAllParty(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<IEnumerable<CandidateDTO>> GetAllPartyAsync(int PartyId)
        {
            var AllGetCandidateParty = await _candidateRepository.GetAllByPartyIdAsync(PartyId);
            var candidateList = new List<CandidateDTO>();

            foreach (var item in AllGetCandidateParty)
            {
                var candidateDTO = new CandidateDTO
                {
                    Name = item.Name.Name,
                    LastName = item.Name.LastName,
                    PhotoUrl = item.PhotoUrl?.PhotoUrl,
                    State = item.State,
                    PoliticalPartyId = item.PoliticalPartyId,
                    HasParticipatedInElection = item.HasParticipatedInElection
                };
                candidateList.Add(candidateDTO);
            }
            return candidateList;
        }
    }
}
