using eVote360.Core.Application.Contracts.Election.Query;
using eVote360.Core.Application.DTOs.Election;

namespace eVote360.Core.Application.Contracts.Election.Query
{
    public interface IGetElectionReportQuery
    {
        Task<IReadOnlyCollection<ElectionResultDto>> ExecuteAsync(int electionId);
    }
}
