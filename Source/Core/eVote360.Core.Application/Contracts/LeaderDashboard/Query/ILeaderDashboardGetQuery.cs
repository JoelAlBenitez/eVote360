using eVote360.Core.Application.DTOs.LeaderDashboard;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Contracts.LeaderDashboard.Query
{
    public interface ILeaderDashboardGetQuery
    {
        Task<LeaderDashboardDto> GetDashboardDataAsync(int userId);
    }
}
