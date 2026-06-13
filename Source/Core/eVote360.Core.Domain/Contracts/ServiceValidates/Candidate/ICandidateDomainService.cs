namespace eVote360.Core.Domain.Contracts.ServiceValidates.Candidate
{


    public interface ICandidateDomainService
    {
    Task<bool> IsElectionProcessActive();
    Task<bool> IsPoliticalPartyActive(int partyId);
    Task<bool> CandidateHasParticipatedInElection(int candidateId);
    Task<bool> CandidateHasPositionAssigned(int candidateId);
    Task<bool> CandidateBelongsToParty(int candidateId, int partyId);
        Task<bool> GetCandidateStateAsync(int candidateId);
        Task<bool> CandidateExistsAsync(int candidateId);
        Task<bool> CandidateNameExistsInParty(string name, string lastName, int partyId, int excludeId);
    }
}
