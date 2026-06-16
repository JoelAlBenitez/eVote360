using eVote360.Core.Application.DTOs.LeaderDashboard;
using eVote360.Core.Domain.Common.ValidationResult;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Contracts.LeaderDashboard.Query
{
    public interface ILeaderDashboardGetQuery
    {
        Task<ValidationResult<LeaderDashboardDto>> GetDashboardDataAsync(int userId);
    }
}
