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
                return (await _context.Elections
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == tkey))!;
            }

            public async Task<bool> UpdateEntitieAsync(ElectionEntity entitie)
            {
                var existing = await _context.Elections.FindAsync(entitie.Id);
                if (existing == null) return false;

                existing.Name = entitie.Name;
                existing.ElectionDate = entitie.ElectionDate;
                existing.ElectionState = entitie.ElectionState;
                existing.State = entitie.State;
                existing.UpdateAt = entitie.UpdateAt;
                existing.UpdateUserId = entitie.UpdateUserId;

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
   
            public async Task<IReadOnlyCollection<ElectionResum>> GetAllElectionsAsync()
            {
            var resum = await _context.Elections
                      .AsNoTracking()
                      .Select(e => new ElectionResum
                      {
                          NameElection = e.Name,
                          Id = e.Id,
                          DateRealized = e.ElectionDate.Value,
                          State = e.ElectionState,
                          NumberElectivePositionsParticipating = _context.Vote
                             .AsNoTracking()
                             .Where(v => v.IdElection == e.Id)
                             .Select(v => v.IdElectivePosiction)
                              .Distinct()
                              .Count(),

                          NumberCitizenParticipating = _context.AuditVote
                              .AsNoTracking()
                              .Where(v => v.IdElection == e.Id)
                              .Select(v => v.IdCitizen)
                              .Distinct()
                              .Count(),

                          NumberParticipatingMatches = _context.Vote.
                           AsNoTracking()
                          .Where(v => v.IdElection == e.Id)
                          .Select(v => v.Candidacte!.PoliticalPartyId)
                          .Distinct()
                          .Count()
                      }).ToListAsync();

            return resum;

        }

        public async Task<ElectionEntity?> GetActivateElectionAsync()
            {
                return await _context.Elections
                   .AsNoTracking()
                   .Include(x => x.ElectivePositions)
                   .FirstOrDefaultAsync(x => x.ElectionState == ElectionState.Activa && x.State == true);
            }

        public async Task<bool> DeactivateElectionAsync(int id)
        {
            var election = await _context.Elections.FindAsync(id);
            if (election == null) return false;

            election.ElectionState = ElectionState.Finalizada;
            election.State = false;
            _context.Elections.Update(election);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ActivateElectionAsync(int id)
        {
            var election = await _context.Elections.FindAsync(id);
            if (election == null) return false;

            election.ElectionState = ElectionState.Activa;
            election.State = true;
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
            var results = await (from vote in _context.Vote
                                 join position in _context.ElectivePosition on vote.IdElectivePosiction equals position.Id
                                 join candidate in _context.Candidates on vote.IdCandidate equals candidate.Id into candidateGroup
                                 from candidate in candidateGroup.DefaultIfEmpty()
                                 join party in _context.PoliticalParties on candidate.PoliticalPartyId equals party.Id into partyGroup
                                 from party in partyGroup.DefaultIfEmpty()
                                 where vote.IdElection == electionId
                                 group vote by new
                                 {
                                     PositionId = position.Id,
                                     PositionName = position.Name,
                                     CandidateId = candidate != null ? (int?)candidate.Id : null,
                                     CandidateFullName = candidate != null ? candidate.Name.Name + " " + candidate.Name.LastName : "Ninguno",
                                     PartyName = party != null ? party.Name : "No aplica",
                                     PartyAcronym = party != null ? party.PoliticalPartyAcronym.Value : "N/A",
                                     PartyLogo = party != null ? party.PoliticalPartyLogo.PhotoUrl : ""
                                 } into g
                                 select new ElectionResult
                                 {
                                     PositionName = g.Key.PositionName,
                                     CandidateNamer = g.Key.CandidateFullName,
                                     PartyName = g.Key.PartyName,
                                     PartyAcronym = g.Key.PartyAcronym,
                                     PartyLogo = g.Key.PartyLogo,
                                     TotalVotes = g.Count()
                                 })
                                 .OrderBy(r => r.PositionName).AsNoTracking()
                                 .ToListAsync();

            return results;
        }

        public async Task<IReadOnlyCollection<ElectionResult>> ElectionByYearAsync(DateTime year)
        {
            var results = await (from vote in _context.Vote
                                 join position in _context.ElectivePosition on vote.IdElectivePosiction equals position.Id
                                 join candidate in _context.Candidates on vote.IdCandidate equals candidate.Id into candidateGroup
                                 from candidate in candidateGroup.DefaultIfEmpty()
                                 join party in _context.PoliticalParties on candidate.PoliticalPartyId equals party.Id into partyGroup
                                 from party in partyGroup.DefaultIfEmpty()
                                 where vote.Elections!.ElectionDate.Value.Year == year.Year
                                 group vote by new
                                 {
                                     PositionId = position.Id,
                                     PositionName = position.Name,
                                     CandidateId = candidate != null ? (int?)candidate.Id : null,
                                     CandidateFullName = candidate != null ? candidate.Name.Name + " " + candidate.Name.LastName : "Ninguno",
                                     PartyName = party != null ? party.Name : "No aplica",
                                     PartyAcronym = party != null ? party.PoliticalPartyAcronym.Value : "N/A",
                                     PartyLogo = party != null ? party.PoliticalPartyLogo.PhotoUrl : ""
                                 } into g
                                 select new ElectionResult
                                 {
                                     PositionName = g.Key.PositionName,
                                     CandidateNamer = g.Key.CandidateFullName,
                                     PartyName = g.Key.PartyName,
                                     PartyAcronym = g.Key.PartyAcronym,
                                     PartyLogo = g.Key.PartyLogo,
                                     TotalVotes = g.Count()
                                 })
                                .OrderBy(r => r.PositionName).AsNoTracking()
                                .ToListAsync();

            return results;
        }
    }
    }

