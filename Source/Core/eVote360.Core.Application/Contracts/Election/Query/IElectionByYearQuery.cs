using eVote360.Core.Application.DTOs.Election;


namespace eVote360.Core.Application.Contracts.Election.Query
{
    public interface IElectionByYearQuery
    {
        Task<IReadOnlyCollection<ElectionDto>> ExecuteAsync(int year);
    }
}
