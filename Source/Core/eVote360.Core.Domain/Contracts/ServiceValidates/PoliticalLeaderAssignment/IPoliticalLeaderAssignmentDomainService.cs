using System.Threading.Tasks;

namespace eVote360.Core.Domain.Contracts.ServiceValidates.PoliticalLeaderAssignment
{
    public interface IPoliticalLeaderAssignmentDomainService
    {
        Task<bool> IsElectionProcessActive();
        Task<bool> UserExists(int userId);
        Task<bool> UserIsActive(int userId);
        Task<bool> UserHasLeaderRole(int userId);
        Task<bool> PartyExists(int partyId);
        Task<bool> PartyIsActive(int partyId);
    }
}
