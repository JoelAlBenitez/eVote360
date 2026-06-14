using eVote360.Core.Application.DTOs.Admin;
using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Contracts.Admin.Query
{
    public interface IElectionByYearQuery
    {
        Task<ValidationResult<IReadOnlyCollection<ElectoralSummaryDto>>> GetRegisterAsync(DateTime year);
    }
}
