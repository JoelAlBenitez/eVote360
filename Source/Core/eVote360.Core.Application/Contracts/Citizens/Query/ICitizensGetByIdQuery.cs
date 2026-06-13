using eVote360.Core.Application.DTOs.Citizens;
using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Contracts.Citizens.Query
{
    public interface ICitizensGetByIdQuery
    {
        Task<ValidationResult<CitizensDto>> GetByIdAsync(Guid Id);
    }
}
