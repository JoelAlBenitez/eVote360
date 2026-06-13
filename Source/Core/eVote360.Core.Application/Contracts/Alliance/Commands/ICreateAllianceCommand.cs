using eVote360.Core.Application.Alliances.DTOs;
using eVote360.Core.Domain.Common.ValidationResult;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Contracts.Alliance.Commands
{
    public interface ICreateAllianceCommand
    {
        Task<ValidationResult<AllianceDto>> ExecuteAsync(CreateAllianceRequestDto request, int requestingPartyId, int authenticatedUserId);
    }
}
