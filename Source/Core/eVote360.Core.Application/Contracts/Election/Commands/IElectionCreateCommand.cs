using eVote360.Core.Application.DTOs.Election;
using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Contracts.Election.Commands
{
    public interface IElectionCreateCommand
    {
        Task<ValidationResult> ExecuteAsync(ElectionDto election);
    }
}
