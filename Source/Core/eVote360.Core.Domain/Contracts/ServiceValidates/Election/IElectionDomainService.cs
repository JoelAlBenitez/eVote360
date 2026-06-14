using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Settings.ValueObjects.ElectionDate;

namespace eVote360.Core.Domain.Contracts.ServiceValidates.Election
{
    public interface IElectionDomainService
    {
        Task<bool> ExistElectionByName(string Name);
        Task<bool> ExistElectionById(int idElection);
        Task<bool> HasEnoughActiveParties();
        Task<bool> HasEnoughActivePositions();
        Task<List<string>> GetPartiesWithMissingCandidates();
        Task<bool> ExistActiveElection();
        Task<bool> ValidElectionDate(ElectionDate electionDate);
        Task<bool> ValidateElectionState(int electionId, ElectionState expectedState);
    }
}
