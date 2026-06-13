using eVote360.Core.Application.DTOs.PoliticalParty;
using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Contracts.PoliticalParty.Commands
{
    public interface IPoliticalPartyStateCommand
    {
        Task<ValidationResult> ExecuteAsync(int id);
    }
}
