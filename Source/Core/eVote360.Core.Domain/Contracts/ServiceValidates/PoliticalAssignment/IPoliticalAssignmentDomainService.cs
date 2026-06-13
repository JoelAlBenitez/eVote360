using eVote360.Core.Domain.Contracts.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Contracts.ServiceValidates.PoliticalAssignment
{
    public interface IPoliticalAssignmentDomainService
    {
        Task<bool> IsPartyActiveAsync(int partyId);
        Task<bool> PartyAlreadyHasLeaderAsync(int partyId);
        Task<bool> UserHasLeaderRoleAsync(int userId);
        Task<bool> UserIsActiveAsync(int userId);
        Task<bool> UserAlreadyAssignedAsync(int userId);

    }
}
