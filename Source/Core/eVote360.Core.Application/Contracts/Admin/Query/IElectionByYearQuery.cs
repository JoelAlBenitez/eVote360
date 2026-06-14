using eVote360.Core.Application.DTOs.Admin;

namespace eVote360.Core.Application.Contracts.Admin.Query
{
    public interface IElectionByYearQuery
    {
        Task<IReadOnlyCollection<AdminDto>> GetRegisterAsync(DateTime year);
    }
}
