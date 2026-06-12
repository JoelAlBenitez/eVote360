using eVote360.Core.Application.DTOs.Citizens;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Entities.Citizens;

namespace eVote360.Core.Application.Contracts.Citizens.Command
{
    public interface ICitizensCreateCommand
    {
         Task<ValidationResult> CreateAsync(CitizensDto citizen);
    }
}
