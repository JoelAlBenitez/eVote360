using eVote360.Core.Application.Alliances.DTOs;
using eVote360.Core.Domain.Common.ValidationResult;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Contracts.Alliance.Commands
{
    public interface IAcceptAllianceCommand
    {
        Task<ValidationResult<AllianceDto>> ExecuteAsync(int allianceId, int authenticatedPartyId);
    }
}
