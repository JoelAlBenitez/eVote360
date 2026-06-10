using eVote360.Core.Application.Contracts.Candidate.Query;
using eVote360.Core.Application.DTOs.Candidates;
using eVote360.Core.Domain.Contracts.Repositories.Candidate;

namespace eVote360.Core.Application.Services.Candidate.Query
{
    public class CandidateGetById : ICandidateGetByIdQuery
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateGetById(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<CandidateDTO> GetByIdAsync(int candidateId, int partyId)
        {
            var candidateById = await _candidateRepository.GetByIdEntitie(candidateId);

            if (candidateById == null)
            {
                return null;
            }

            if (candidateById.PoliticalPartyId != partyId) return null;

            var DtoCandidate = new CandidateDTO
            {
                Name = candidateById.Name.Name,
                LastName = candidateById.Name.LastName,
                PhotoUrl = candidateById.PhotoUrl?.PhotoUrl,
                State = candidateById.State,
                PoliticalPartyId = candidateById.PoliticalPartyId,
                HasParticipatedInElection = candidateById.HasParticipatedInElection
            };

            return DtoCandidate;
        }
    }
}
