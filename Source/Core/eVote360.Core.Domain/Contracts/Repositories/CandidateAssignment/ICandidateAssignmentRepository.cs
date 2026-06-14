using CandidateAssignmentEntity = eVote360.Core.Domain.Entities.CandidateAssignment.CandidateAssignment;
using eVote360.Core.Domain.Entities.Candidate;
using eVote360.Core.Domain.Entities.CandidateAssignment.ReadModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Contracts.Repositories.CandidateAssignment
{
    public interface ICandidateAssignmentRepository
    {
        Task<bool> CreateEntiteAsync(CandidateAssignmentEntity entitie);
        Task<bool> DeleteAsync(int assignmentId);
        Task<CandidateAssignmentEntity> GetByIdEntitie(int assignmentId);
        
        // Métodos de lectura con JOINs (Read Models)
        Task<IEnumerable<CandidateAssignmentReadModel>> GetAllByPartyIdAsync(int partyId);
        Task<CandidateAssignmentReadModel?> GetByIdWithDetailsAsync(int assignmentId);

        Task<bool> ExistsAssignmentForCandidateInParty(int candidateId, int partyId);
        Task<bool> ExistsAssignmentForPositionInParty(int electivePositionId, int partyId);
        Task<CandidateAssignmentEntity?> GetAssignmentByCandidateInParty(int candidateId, int partyId);
        Task<IEnumerable<Candidates>> GetEligibleCandidatesAsync(int partyId, int electivePositionId);
    }
}
