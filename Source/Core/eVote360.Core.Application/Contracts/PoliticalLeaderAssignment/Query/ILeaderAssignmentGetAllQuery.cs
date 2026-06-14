using eVote360.Core.Application.DTOs.PoliticalLeaderAssignment;

namespace eVote360.Core.Application.Contracts.PoliticalLeaderAssignment.Query
{
    public interface ILeaderAssignmentGetAllQuery
    {
        Task<IReadOnlyCollection<LeaderAssignmentDto>> ExecuteAsync();
    }
}
