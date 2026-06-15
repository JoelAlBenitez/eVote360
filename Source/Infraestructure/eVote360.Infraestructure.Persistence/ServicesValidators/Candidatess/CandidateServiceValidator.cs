using eVote360.Core.Domain.Contracts.ServiceValidates.Candidate;
using eVote360.Core.Domain.Common.Enums;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace eVote360.Infraestructure.Persistence.ServicesValidators.Candidatess
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
            return await _context.Elections
                .AsNoTracking()
                .AnyAsync(x => x.ElectionState == ElectionState.Activa);
        }

        public async Task<bool> IsPoliticalPartyActive(int partyId)
        {
            return await _context.PoliticalParties
                .AsNoTracking()
                .AnyAsync(x => x.Id == partyId && x.State == true);
        }

        public async Task<bool> CandidateHasParticipatedInElection(int candidateId)
        {
            return await _context.Candidates
                .AsNoTracking()
                .AnyAsync(x => x.Id == candidateId && x.HasParticipatedInElection);
        }

        public async Task<bool> CandidateHasPositionAssigned(int candidateId)
        {
            return await _context.CandidateAssignments
                .AsNoTracking()
                .AnyAsync(x => x.CandidateId == candidateId);
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
        public async Task<bool> GetCandidateStateAsync(int candidateId)
        {
            var candidate = await _context.Candidates
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == candidateId);

            if (candidate == null) return false;
            return candidate.State;
        }

        public async Task<bool> CandidateExistsAsync(int candidateId)
        {
            return await _context.Candidates
                .AsNoTracking()
                .AnyAsync(x => x.Id == candidateId);
        }
    }
}
