using eVote360.Core.Application.DTOs.Election;
using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Contracts.Admin.Query
{
    public interface IAvailableYearsQuery
    {
        Task< ValidationResult<IReadOnlyCollection<ElectionDate>>> AvailableYearAsync();
    }
}
