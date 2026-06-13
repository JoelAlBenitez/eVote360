using eVote360.Core.Domain.Contracts.ServiceValidates.PoliticalAlliance;
using eVote360.Core.Domain.Entities.PoliticalAlliances;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eVote360.Infraestructure.Persistence.ServicesValidators
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
            // pendiente hasta traer entidad de elecciones
            return await Task.FromResult(false);
        }

        public async Task<bool> IsPartyActive(int partyId)
        {
            // pendiente hasta traer entidad de partidos
            return await Task.FromResult(true);
        }

        public async Task<bool> HasActiveAlliance(int requestingPartyId, int receivingPartyId)
        {
            return await _context.PoliticalAlliances
                .AsNoTracking()
                .AnyAsync(x => x.Status == AllianceStatus.Accepted && 
                               ((x.RequestingPartyId == requestingPartyId && x.ReceivingPartyId == receivingPartyId) ||
                                (x.RequestingPartyId == receivingPartyId && x.ReceivingPartyId == requestingPartyId)));
        }

        public async Task<bool> HasPendingRequest(int requestingPartyId, int receivingPartyId)
        {
            return await _context.PoliticalAlliances
                .AsNoTracking()
                .AnyAsync(x => x.Status == AllianceStatus.Pending && 
                               ((x.RequestingPartyId == requestingPartyId && x.ReceivingPartyId == receivingPartyId) ||
                                (x.RequestingPartyId == receivingPartyId && x.ReceivingPartyId == requestingPartyId)));
        }

        public async Task<bool> HasAssignedCandidatesBetweenParties(int requestingPartyId, int receivingPartyId)
        {
            // pendiente hasta traer mdulo de asignacioon
            return await Task.FromResult(false);
        }
    }
}
