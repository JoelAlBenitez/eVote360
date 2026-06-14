using eVote360.Core.Domain.Contracts.Repositories.CandidateAssignment;
using eVote360.Core.Domain.Entities.Candidate;
using eVote360.Core.Domain.Entities.PoliticalAlliances;
using eVote360.Core.Domain.Entities.CandidateAssignment.ReadModels;
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

        public async Task<IEnumerable<CandidateAssignmentReadModel>> GetAllByPartyIdAsync(int partyId)
        {
            
            var query = from ep in _context.ElectivePosition.Where(e => e.State == true)
                        join ca in _context.CandidateAssignments.Where(c => c.AssigningPartyId == partyId)
                            on ep.Id equals ca.ElectivePositionId into caGroup
                        from caLeft in caGroup.DefaultIfEmpty()
                        
                        join c in _context.Candidates on (caLeft != null ? caLeft.CandidateId : 0) equals c.Id into cGroup
                        from cLeft in cGroup.DefaultIfEmpty()

                   

                        select new CandidateAssignmentReadModel
                        {
                            ElectivePositionId = ep.Id,
                            ElectivePositionName = ep.Description,
                            
                            AssignmentId = caLeft != null ? (int?)caLeft.Id : null,
                            CandidateId = caLeft != null ? (int?)caLeft.CandidateId : null,
                            
                            CandidateName = cLeft != null ? cLeft.Name.Name : null,
                            CandidateLastName = cLeft != null ? cLeft.Name.LastName : null,
                            PhotoUrl = cLeft != null && cLeft.PhotoUrl != null ? cLeft.PhotoUrl.PhotoUrl : null,
                            
                            CandidateType = cLeft != null ? (cLeft.PoliticalPartyId == partyId ? "Propio" : "Aliado") : null,
                            
                            // Nombres de partidos origen no disponibles si no hay DbSet PoliticalParties en el DbContext
                            OriginPartyName = cLeft != null ? "Partido " + cLeft.PoliticalPartyId : null,
                            OriginPartySiglas = cLeft != null ? "P" + cLeft.PoliticalPartyId : null,

                            AssigningPartyId = partyId
                        };

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<CandidateAssignmentReadModel?> GetByIdWithDetailsAsync(int assignmentId)
        {
            var query = from ca in _context.CandidateAssignments.Where(c => c.Id == assignmentId)
                        join ep in _context.ElectivePosition on ca.ElectivePositionId equals ep.Id
                        join c in _context.Candidates on ca.CandidateId equals c.Id
                        
                        select new CandidateAssignmentReadModel
                        {
                            ElectivePositionId = ep.Id,
                            ElectivePositionName = ep.Description,
                            
                            AssignmentId = ca.Id,
                            CandidateId = ca.CandidateId,
                            
                            CandidateName = c.Name.Name,
                            CandidateLastName = c.Name.LastName,
                            PhotoUrl = c.PhotoUrl != null ? c.PhotoUrl.PhotoUrl : null,
                            
                            CandidateType = c.PoliticalPartyId == ca.AssigningPartyId ? "Propio" : "Aliado",
                            
                            OriginPartyName = "Partido " + c.PoliticalPartyId,
                            OriginPartySiglas = "P" + c.PoliticalPartyId,

                            AssigningPartyId = ca.AssigningPartyId
                        };

            return await query.AsNoTracking().FirstOrDefaultAsync();
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

           
            // AQUI Usare POLITICALALLIANCES PERO NO ESTA EN DBCONTEXT EN ESTA RAMA
            // Para que compile, devolveremos lista vacia de aliados temporalmente.
            var alliedPartyIds = new List<int>(); 

            
            var eligibleAlliedCandidateIds = await _context.CandidateAssignments
                .AsNoTracking()
                .Where(x => alliedPartyIds.Contains(x.AssigningPartyId) && 
                            x.ElectivePositionId == electivePositionId)
                .Select(x => x.CandidateId)
                .ToListAsync();

            return await _context.Candidates
                .AsNoTracking()
                .Where(x => x.State == true &&
                           !assignedCandidateIds.Contains(x.Id) &&
                           (x.PoliticalPartyId == partyId || eligibleAlliedCandidateIds.Contains(x.Id)))
                .ToListAsync();
        }
    }
}
