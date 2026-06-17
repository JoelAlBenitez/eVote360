using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Contracts.Repositories.ElectionRepository;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectionEntity = eVote360.Core.Domain.Entities.Election.Election;
using eVote360.Core.Domain.Entities.Elector.Vote;
using eVote360.Core.Domain.Entities.Election;

namespace eVote360.Infraestructure.Persistence.Repositories.Election
{
    public class ElectionRepository : IElectionRepository
    {
        protected readonly DbContextEVote360 _context;

        public ElectionRepository(DbContextEVote360 context) {
        _context = context;
        }

        public async Task<bool> CreateEntiteAsync(ElectionEntity entitie)
            {
                _context.Elections.Add(entitie);
                return await _context.SaveChangesAsync() > 0;
            }
   
            public async Task<ElectionEntity> GetByIdEntitie(int tkey)
            {
                return await _context.Elections
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == tkey);
            }

            public async Task<bool> UpdateEntitieAsync(ElectionEntity entitie)
            {
                 _context.Update(entitie);
                 return await _context.SaveChangesAsync() > 0;
            }

            public async Task<bool> AlterState(int tkey, bool state)
            {
                 var election = await _context.Elections.FindAsync(tkey);
                 if (election == null) return false;
    
                election.State = state;
                 _context.Elections.Update(election);
                 return await _context.SaveChangesAsync() > 0;
             }
   
            public async Task<IEnumerable<ElectionEntity>> GetAllElectionsAsync()
            {
                 return await _context.Elections
                   .AsNoTracking()
                   .ToListAsync();
            }

            public async Task<ElectionEntity?> GetActivateElectionAsync()
            {
                return await _context.Elections
                   .AsNoTracking()
                   .Include(x => x.ElectivePositions)
                   .FirstOrDefaultAsync(x => x.ElectionState == ElectionState.Activa);
            }

        public async Task<bool> DeactivateElectionAsync(int id)
        {
            var election = await _context.Elections.FindAsync(id);
            if (election == null) return false;

            election.ElectionState = ElectionState.Finalizada;
            _context.Elections.Update(election);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IReadOnlyCollection<ElectionEntity>> GetElectionsByYearAsync(int year)
        {
            return await _context.Elections
                .AsNoTracking()
                .Where(x =>x.ElectionDate.Value.Year == year)
                .ToListAsync();
        }
        public async Task<IReadOnlyCollection<ElectionResult>> GetElectionResultAsync(int electionId)
        {
            var totalVotesCount = await _context.Vote.Where(v=>v.IdElection == electionId)
            .CountAsync();

            var results = await (from vote in _context.Vote
                                 join candidate in _context.Candidates on vote.IdCandidate equals candidate.Id
                                 join party in _context.PoliticalParties on candidate.PoliticalPartyId equals party.Id
                                 where vote.IdElection == electionId
                                 group vote by new {
                                 CandidateId = candidate.Id,
                                 CandidateObj = candidate.Name,
                                 PartyId = party.Id,
                                 PartyName = party.Name,
                                 PartyAcronym = party.PoliticalPartyAcronym,
                                 PartyLog = party.PoliticalPartyLogo.PhotoUrl,
                                 } into g
                                 select new ElectionResult
                                 {
                                     CandidateNamer = g.Key.CandidateObj.Name + " " + g.Key.CandidateObj.LastName,
                                     PartyName = g.Key.PartyName,
                                     PartyAcronym = g.Key.PartyAcronym.Value,
                                     PartyLogo = g.Key.PartyLog,
                                     TotalVotes = g.Count(),
                                     Percentage = totalVotesCount > 0 ? (double)g.Count() / totalVotesCount * 100 : 0
                                 })
                                 .OrderByDescending(r=>r.TotalVotes)
                                 .ToListAsync();

            return results;
           
        }
    }
}
