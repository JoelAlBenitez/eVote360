using eVote360.Core.Domain.ValueObjects;
using eVote360.Core.Domain.Common.Enums;

namespace eVote360.Core.Domain.Contracts.DomainService.Election
{
    public interface IElectionDomainService
    {
        Task<bool> ExistElectionByName(string Name);
        Task<bool> ExistElectionById(int idElection);
        Task<bool> ElectionHasEnoughParties (int idElection);
        Task<bool> ExistActiveElection();
        Task<bool> ValidElectionDate(ElectionDate electionDate);
        Task<bool> ValidateElectionState(int electionId, ElectionState expectedState);
    }
}
