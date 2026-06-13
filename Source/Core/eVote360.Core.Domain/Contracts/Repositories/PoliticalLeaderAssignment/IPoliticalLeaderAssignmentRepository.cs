using eVote360.Core.Domain.Entities.PoliticalLeaderAssignment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Contracts.Repositories.PoliticalLeaderAssignment
{
    public interface IPoliticalLeaderAssignmentRepository
    {
        Task<bool> CreateEntiteAsync(Core.Domain.Entities.PoliticalLeaderAssignment.PoliticalLeaderAssignment entitie);
        Task<bool> DeleteAsync(int assignmentId);
        Task<Core.Domain.Entities.PoliticalLeaderAssignment.PoliticalLeaderAssignment?> GetByIdEntitie(int assignmentId);
        Task<IEnumerable<Core.Domain.Entities.PoliticalLeaderAssignment.PoliticalLeaderAssignment>> GetAllAsync();
        Task<bool> ExistsAssignmentForUser(int userId);
        Task<bool> ExistsAssignmentForParty(int partyId);
        Task<Core.Domain.Entities.PoliticalLeaderAssignment.PoliticalLeaderAssignment?> GetByUserId(int userId);
        Task<Core.Domain.Entities.PoliticalLeaderAssignment.PoliticalLeaderAssignment?> GetByPartyId(int partyId);
        
        // Métodos agregados para soportar los dropdowns solicitados en la corrección
        Task<IEnumerable<dynamic>> GetUnassignedLeadersAsync();
        Task<IEnumerable<dynamic>> GetActivePartiesWithoutLeaderAsync();
    }
}
