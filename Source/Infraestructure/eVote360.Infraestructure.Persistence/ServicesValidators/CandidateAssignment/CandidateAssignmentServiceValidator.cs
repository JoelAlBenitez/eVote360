using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Contracts.ServiceValidates.CandidateAssignment;
using eVote360.Core.Domain.Entities.PoliticalAlliances;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eVote360.Infraestructure.Persistence.ServicesValidators.CandidateAssignment
{
    public class CandidateAssignmentServiceValidator : ICandidateAssignmentDomainService
    {
        private readonly DbContextEVote360 _context;

        public CandidateAssignmentServiceValidator(DbContextEVote360 context)
        {
            _context = context;
        }

        public async Task<bool> ElectivePositionHasAssociatedByCandidates(int Id)
        {
            return await _context.CandidateAssignments.AsNoTracking()
                .AnyAsync(x => x.ElectivePositionId == Id);

        }


        public async Task<bool> IsElectionProcessActive()
        {
            return await _context.Elections
                .AsNoTracking()
                .AnyAsync(x => x.ElectionState == ElectionState.Activa);
        }

        public async Task<bool> IsPartyActive(int partyId)
        {
            return await _context.PoliticalParties
                .AsNoTracking()
                .AnyAsync(x => x.Id == partyId && x.State == true);
        }

        public async Task<bool> UserExists(int userId)
        {
            return await _context.Users
                .AsNoTracking()
                .AnyAsync(x => x.Id == userId);
        }

        public async Task<bool> UserIsActive(int userId)
        {
            return await _context.Users
                .AsNoTracking()
                .AnyAsync(x => x.Id == userId && x.State == true);
        }

        public async Task<bool> UserHasLeaderRole(int userId)
        {
            return await _context.Users
                .AsNoTracking()
                .AnyAsync(x => x.Id == userId && x.UserRole == UserRole.DirigentePolitico);
        }

        public async Task<bool> PartyExists(int partyId)
        {
            return await _context.PoliticalParties
                .AsNoTracking()
                .AnyAsync(x => x.Id == partyId);
        }

        public async Task<bool> CandidateIsActive(int candidateId)
        {
            return await _context.Candidates
                .AsNoTracking()
                .AnyAsync(x => x.Id == candidateId && x.State == true);
        }

        public async Task<bool> ElectivePositionIsActive(int electivePositionId)
        {
            return await _context.ElectivePosition
                .AsNoTracking()
                .AnyAsync(x => x.Id == electivePositionId && x.State == true);
        }

        public async Task<bool> CandidateBelongsToParty(int candidateId, int partyId)
        {
            return await _context.Candidates
                .AsNoTracking()
                .AnyAsync(x => x.Id == candidateId && x.PoliticalPartyId == partyId);
        }

        public async Task<bool> ExistsActiveAllianceBetweenParties(int partyId1, int partyId2)
        {
            return await _context.PoliticalAlliances
                .AsNoTracking()
                .AnyAsync(x => x.Status == AllianceStatus.Aceptado &&
                               ((x.RequestingPartyId == partyId1 && x.ReceivingPartyId == partyId2) ||
                                (x.RequestingPartyId == partyId2 && x.ReceivingPartyId == partyId1)));
        }

        public async Task<bool> CandidateHasAssignmentInOriginParty(int candidateId, int originPartyId)
        {
            return await _context.CandidateAssignments
                .AsNoTracking()
                .AnyAsync(x => x.CandidateId == candidateId && x.AssigningPartyId == originPartyId);
        }

        public async Task<int?> GetCandidatePositionInOriginParty(int candidateId, int originPartyId)
        {
            var assignment = await _context.CandidateAssignments
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CandidateId == candidateId && x.AssigningPartyId == originPartyId);
            
            return assignment?.ElectivePositionId;
        }
    }
}
