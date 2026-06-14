using eVote360.Core.Application.DTOs.Election;
namespace eVote360.Core.Application.Contracts.Election.Query
{
    public interface IElectionGetAllQuery
    {
        Task<IReadOnlyCollection<ElectionResumDto>> ExecuteAsync();
    }
}
