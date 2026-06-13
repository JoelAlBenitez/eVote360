using eVote360.Core.Domain.Contracts.DomainService.Candidate;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eVote360.Infraestructure.Persistence.ServicesValidators
{
    public class CandidateServiceValidator : ICandidateDomainService
    {
        private readonly DbContextEVote360 _context;

        public CandidateServiceValidator(DbContextEVote360 context)
        {
            _context = context;
        }

        public async Task<bool> IsElectionProcessActive()
        {
            // TODO: Integrar con el modulo de Elecciones cuando se cree la tabla.
            // Por ahora retornamos false para permitir operaciones.
            return await Task.FromResult(false); 
        }

        public async Task<bool> IsPoliticalPartyActive(int partyId)
        {
            // Integrar con el modulo de Partidos Politicos de Sebastian.
            // Por ahora simulamos que el partido esta activo
            return await Task.FromResult(true);
        }

        public async Task<bool> CandidateHasParticipatedInElection(int candidateId)
        {
            return await _context.Candidates
                .AsNoTracking()
                .AnyAsync(x => x.Id == candidateId && x.HasParticipatedInElection);
        }

        public async Task<bool> CandidateHasPositionAssigned(int candidateId)
        {
            // Integrar con la tabla de Alianzas o Inscripciones.
            return await Task.FromResult(false);
        }

        public async Task<bool> CandidateBelongsToParty(int candidateId, int partyId)
        {
            return await _context.Candidates
                .AsNoTracking()
                .AnyAsync(x => x.Id == candidateId && x.PoliticalPartyId == partyId);
        }

        public async Task<bool> CandidateNameExistsInParty(string name, string lastName, int partyId, int excludeId)
        {
            return await _context.Candidates
                .AsNoTracking()
                .AnyAsync(x => x.Name.Name == name && 
                               x.Name.LastName == lastName && 
                               x.PoliticalPartyId == partyId && 
                               x.Id != excludeId);
        }
    }
}
