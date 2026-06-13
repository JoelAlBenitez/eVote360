using eVote360.Core.Domain.Contracts.Repositories.CandidateAssignment;
using eVote360.Core.Domain.Entities.Candidate;
using eVote360.Core.Domain.Entities.PoliticalAlliances;
using CandidateAssignmentEntity = eVote360.Core.Domain.Entities.CandidateAssignment.CandidateAssignment;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVote360.Infraestructure.Persistence.Repositories.CandidateAssignment
{
    public class CandidateAssignmentRepository : ICandidateAssignmentRepository
    {
        private readonly DbContextEVote360 _context;

        public CandidateAssignmentRepository(DbContextEVote360 context)
        {
            _context = context;
        }

        public async Task<bool> CreateEntiteAsync(CandidateAssignmentEntity entitie)
        {
            _context.CandidateAssignments.Add(entitie);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int assignmentId)
        {
            var entity = await _context.CandidateAssignments
                .FirstOrDefaultAsync(x => x.Id == assignmentId);
            
            if (entity == null) return false;

            _context.CandidateAssignments.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<CandidateAssignmentEntity> GetByIdEntitie(int assignmentId)
        {
            var entity = await _context.CandidateAssignments
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == assignmentId);
            
            return entity!;
        }

        public async Task<IEnumerable<CandidateAssignmentEntity>> GetAllByPartyIdAsync(int partyId)
        {
            return await _context.ElectivePosition
                .AsNoTracking()
                .Where(ep => ep.State == true)
                .GroupJoin(
                    _context.CandidateAssignments.Where(ca => ca.AssigningPartyId == partyId),
                    ep => ep.Id,
                    ca => ca.ElectivePositionId,
                    (ep, caGroup) => new { ep, caGroup }
                )
                .SelectMany(
                    x => x.caGroup.DefaultIfEmpty(),
                    (x, ca) => new CandidateAssignmentEntity
                    {
                        Id = ca != null ? ca.Id : 0,
                        ElectivePositionId = x.ep.Id,
                        AssigningPartyId = partyId,
                        CandidateId = ca != null ? ca.CandidateId : 0,
                        CreateUserId = ca != null ? ca.CreateUserId : 0
                    }
                )
                .ToListAsync();
        }

        public async Task<bool> ExistsAssignmentForCandidateInParty(int candidateId, int partyId)
        {
            return await _context.CandidateAssignments
                .AsNoTracking()
                .AnyAsync(x => x.CandidateId == candidateId && x.AssigningPartyId == partyId);
        }

        public async Task<bool> ExistsAssignmentForPositionInParty(int electivePositionId, int partyId)
        {
            return await _context.CandidateAssignments
                .AsNoTracking()
                .AnyAsync(x => x.ElectivePositionId == electivePositionId && x.AssigningPartyId == partyId);
        }

        public async Task<CandidateAssignmentEntity?> GetAssignmentByCandidateInParty(int candidateId, int partyId)
        {
            return await _context.CandidateAssignments
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CandidateId == candidateId && x.AssigningPartyId == partyId);
        }

        public async Task<IEnumerable<Candidates>> GetEligibleCandidatesAsync(int partyId, int electivePositionId)
        {
            // Paso 1: IDs de candidatos ya asignados en el partido
            var assignedCandidateIds = await _context.CandidateAssignments
                .AsNoTracking()
                .Where(x => x.AssigningPartyId == partyId)
                .Select(x => x.CandidateId)
                .ToListAsync();

            // Paso 2: IDs de partidos aliados vigentes
            var alliedPartyIds = await _context.PoliticalAlliances
                .AsNoTracking()
                .Where(x => x.Status == AllianceStatus.Accepted &&
                           (x.RequestingPartyId == partyId || x.ReceivingPartyId == partyId))
                .Select(x => x.RequestingPartyId == partyId ? x.ReceivingPartyId : x.RequestingPartyId)
                .ToListAsync();

            // Paso 3: IDs de candidatos aliados que tienen el MISMO puesto en su partido de origen
            var eligibleAlliedCandidateIds = await _context.CandidateAssignments
                .AsNoTracking()
                .Where(x => alliedPartyIds.Contains(x.AssigningPartyId) && 
                            x.ElectivePositionId == electivePositionId)
                .Select(x => x.CandidateId)
                .ToListAsync();

            // Paso 4: Candidatos propios activos no asignados + candidatos aliados elegibles
            return await _context.Candidates
                .AsNoTracking()
                .Where(x => x.State == true &&
                           !assignedCandidateIds.Contains(x.Id) &&
                           (x.PoliticalPartyId == partyId || eligibleAlliedCandidateIds.Contains(x.Id)))
                .ToListAsync();
        }
    }
}
