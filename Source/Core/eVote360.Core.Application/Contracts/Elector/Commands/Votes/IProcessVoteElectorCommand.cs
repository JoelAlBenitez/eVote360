using eVote360.Core.Application.DTOs.Elector.Select;
using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Contracts.Elector.Commands.Votes
{
    public interface IProcessVoteElectorCommand
    {
        Task<ValidationResult> ProcessVoteAsync(List<SelectedVoteDto> selectedVotes);
    }
}
