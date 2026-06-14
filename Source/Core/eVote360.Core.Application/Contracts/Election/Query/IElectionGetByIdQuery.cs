using eVote360.Core.Application.DTOs.Election;
using eVote360.Core.Domain.Common.ValidationResult;


namespace eVote360.Core.Application.Contracts.Election.Query
{
    public interface IElectionGetByIdQuery
    {
        Task<ValidationResult<ElectionDto>> ExecuteAsync(int id);
    }
}
