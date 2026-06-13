using eVote360.Core.Domain.Common.ValidationResult;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Contracts.Alliance.Commands
{
    public interface IDeleteAllianceRequestCommand
    {
        Task<ValidationResult<bool>> ExecuteAsync(int allianceId, int authenticatedPartyId);
    }
}
