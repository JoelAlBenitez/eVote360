using eVote360.Core.Application.Alliances.DTOs;
using eVote360.Core.Domain.Common.ValidationResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Contracts.Alliance.Query
{
    public interface IGetSentAllianceRequestsQuery
    {
        Task<ValidationResult<IEnumerable<AllianceDto>>> ExecuteAsync(int authenticatedPartyId);
    }
}
