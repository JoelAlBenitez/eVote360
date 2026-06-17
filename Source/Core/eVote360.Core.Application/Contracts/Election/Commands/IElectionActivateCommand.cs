using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Contracts.Election.Commands
{
    public interface IElectionActivateCommand
    {
        Task<ValidationResult> ExecuteAsync(int id);
    }
}
