using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Contracts.Citizens.Command
{
    public interface ICitizensAlterStateCommand
    {
        Task<ValidationResult> AlterStateAsync(Guid Id);
    }
}
