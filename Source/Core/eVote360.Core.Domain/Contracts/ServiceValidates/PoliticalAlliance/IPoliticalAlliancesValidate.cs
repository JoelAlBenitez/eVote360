using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Contracts.ServiceValidates.PoliticalAlliance
{
    public interface IPoliticalAlliancesValidate
    {

        Task<bool> IsElectionProcessActive();
        Task<bool> IsPartyActive(int partyId);
        Task<bool> HasActiveAlliance(int requestingPartyId, int ReceivingPartyId);
        Task<bool> HasPendingRequest(int requestingPartyId, int ReceivingPartyId);
        Task<bool> HasAssignedCandidatesBetweenParties(int requestingPartyId, int ReceivingPartyId);
    }
}
