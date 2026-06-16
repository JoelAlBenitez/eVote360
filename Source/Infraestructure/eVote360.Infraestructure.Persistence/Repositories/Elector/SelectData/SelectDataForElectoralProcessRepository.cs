using eVote360.Core.Domain.Contracts.Repositories.Elector.SelectPorcess;
using eVote360.Core.Domain.Entities.Elector.ElectorData;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace eVote360.Infraestructure.Persistence.Repositories.Elector.SelectData
{
    public class SelectDataForElectoralProcessRepository : ISelectDataForElectoralProcessRepository
    {
        private readonly DbContextEVote360 _context;

        public SelectDataForElectoralProcessRepository(DbContextEVote360 context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<ElectorDataElectionPosition>> GetElectorDataElectionPositionsAsync()
        {
             var result = await _context.ElectivePosition
                .AsNoTracking()
                .Where(e => e.State == true)
                .Select( e => new ElectorDataElectionPosition
                {
                     IdElectivePosition = e.Id,
                     NameElectivePosiction = e.Name,
                     NumberActualCandidacte = _context.CandidateAssignments
                        .AsNoTracking()
                        .Where(c => c.ElectivePositionId  == e.Id)
                        .Select(c => c.CandidateId)
                        .Distinct()
                        .Count(),
                      NumberPoliticalPartyParticiped = _context.CandidateAssignments
                     .AsNoTracking()
                     .Where(p => p.ElectivePositionId == e.Id)
                     .Select(p => p.AssigningPartyId)
                     .Distinct()
                     .Count()
                     

                })
                .ToListAsync();
            return result;
        }

        public async Task<IReadOnlyCollection<ElectorSelectCandidacteElectivepPosiction>> GetElectorSelectCandidacteElectivepPosictionsAsync(int IdElectivePosition)
        {
            var result = await _context.CandidateAssignments
                .AsNoTracking()
                .Where(c => c.ElectivePositionId == IdElectivePosition && c.Candidate!.State == true)
                .Select( c => new ElectorSelectCandidacteElectivepPosiction { 
                       IdCandidate = c.CandidateId,
                       NameCandidacte = c.Candidate!.Name.Name!,
                       PhotoUrlOfCandidacte = c.Candidate!.PhotoUrl.PhotoUrl!,
                       PoliticalParty = c.AssigningParty!.Name,
                       LogoPoliticalParty = c.AssigningParty.PoliticalPartyLogo.PhotoUrl!
                })
                .ToListAsync(); 
            return result;
        }
    }
}
