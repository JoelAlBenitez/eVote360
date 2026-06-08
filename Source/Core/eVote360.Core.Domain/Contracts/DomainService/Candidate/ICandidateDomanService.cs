
namespace eVote360.Core.Domain.Contracts.DomainService.Canditate
{


    public interface ICandidateDomainService
{
    Task<bool> IsElectionProcessActive();
    Task<bool> IsPoliticalPartyActive(int partyId);
    Task<bool> CandidateHasParticipatedInElection(int candidateId);
    Task<bool> CandidateHasPositionAssigned(int candidateId);
    Task<bool> CandidateBelongsToParty(int candidateId, int partyId);
}
}