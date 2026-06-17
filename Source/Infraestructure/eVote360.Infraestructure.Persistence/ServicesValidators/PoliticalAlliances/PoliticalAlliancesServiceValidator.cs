using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Contracts.ServiceValidates.PoliticalAlliance;

using eVote360.Core.Domain.Entities.PoliticalAlliances;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eVote360.Infraestructure.Persistence.ServicesValidators.PoliticalAlliances
{
    public class PoliticalAlliancesServiceValidator : IPoliticalAlliancesValidate
    {
        private readonly DbContextEVote360 _context;

        public PoliticalAlliancesServiceValidator(DbContextEVote360 context)
        {
            _context = context;
        }

        public async Task<bool> IsElectionProcessActive()
        {
            return await _context.Elections
                .AsNoTracking()
                .AnyAsync(x => x.ElectionState == ElectionState.Activa && x.State == true);
        }

        public async Task<bool> IsPartyActive(int partyId)
        {
            return await _context.PoliticalParties
                .AsNoTracking()
                .AnyAsync(x => x.Id == partyId && x.State == true);
        }

        public async Task<bool> HasActiveAlliance(int requestingPartyId, int receivingPartyId)
        {
            return await _context.PoliticalAlliances
                .AsNoTracking()
                .AnyAsync(x => x.Status == AllianceStatus.Aceptado && 
                               (x.RequestingPartyId == requestingPartyId && x.ReceivingPartyId == receivingPartyId ||
                                x.RequestingPartyId == receivingPartyId && x.ReceivingPartyId == requestingPartyId));
        }

        public async Task<bool> HasPendingRequest(int requestingPartyId, int receivingPartyId)
        {
            return await _context.PoliticalAlliances
                .AsNoTracking()
                .AnyAsync(x => x.Status == AllianceStatus.Pendiente && 
                               (x.RequestingPartyId == requestingPartyId && x.ReceivingPartyId == receivingPartyId ||
                                x.RequestingPartyId == receivingPartyId && x.ReceivingPartyId == requestingPartyId));
        }

        public async Task<bool> HasAssignedCandidatesBetweenParties(int requestingPartyId, int receivingPartyId)
        {
            return await _context.CandidateAssignments
                .AsNoTracking()
                .AnyAsync(x =>
                    x.AssigningPartyId == requestingPartyId &&
                     _context.Candidates.Any(c => c.Id == x.CandidateId && c.PoliticalPartyId == receivingPartyId)
                    ||
                    x.AssigningPartyId == receivingPartyId &&
                     _context.Candidates.Any(c => c.Id == x.CandidateId && c.PoliticalPartyId == requestingPartyId));
        }
    }
}
