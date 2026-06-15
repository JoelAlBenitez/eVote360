using System.Threading.Tasks;

namespace eVote360.Core.Domain.Contracts.ServiceValidates.CandidateAssignment
{
    public interface ICandidateAssignmentDomainService
    {
        Task<bool> IsElectionProcessActive();
        Task<bool> IsPartyActive(int partyId);
        Task<bool> CandidateIsActive(int candidateId);
        Task<bool> ElectivePositionIsActive(int electivePositionId);
        Task<bool> CandidateBelongsToParty(int candidateId, int partyId);
        Task<bool> ExistsActiveAllianceBetweenParties(int partyId1, int partyId2);
        Task<bool> CandidateHasAssignmentInOriginParty(int candidateId, int originPartyId);
        Task<int?> GetCandidatePositionInOriginParty(int candidateId, int originPartyId);
    }
}
