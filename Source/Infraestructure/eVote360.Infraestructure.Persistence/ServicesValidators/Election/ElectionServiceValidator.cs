using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Contracts.ServiceValidates.Election;
using eVote360.Core.Domain.Entities.Election;
using eVote360.Core.Domain.Settings.ValueObjects.ElectionDate;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Infraestructure.Persistence.ServicesValidators.Election
{
    public class ElectionServiceValidator : IElectionDomainService
    {
        private readonly DbContextEVote360 _context;

        public ElectionServiceValidator(DbContextEVote360 context) {
        _context = context;
        }

        public async Task<bool> ExistElectionByName(string Name)
        {
                return await _context.Elections.AnyAsync(x => x.Name == Name);
        }
   
        public async Task<bool> ExistElectionById(int idElection)
        {
                return await _context.Elections.AnyAsync(x => x.Id == idElection);
        }

        public async Task<bool> HasEnoughActiveParties()
        {
            return await _context.PoliticalParties.CountAsync(x => x.State) >= 2;
        }

        public async Task<bool> HasEnoughActivePositions()
        {
            return await _context.ElectivePosition.AnyAsync(x => x.State);
        }

        public async Task<List<string>> GetPartiesWithMissingCandidates()
        {
            var activeParties = await _context.PoliticalParties.Where(p => p.State).ToListAsync();
            var activePositions = await _context.ElectivePosition.Where(p => p.State).ToListAsync();
            var missing = new List<string>();

            foreach (var party in activeParties)
            {
                var missingPositions = new List<string>();
                foreach (var position in activePositions)
                {
                    var hasCandidate = await _context.CandidateAssignments
                        .AnyAsync(ca => ca.AssigningPartyId == party.Id && ca.ElectivePositionId == position.Id && ca.Candidate!.State);

                    if (!hasCandidate)
                    {
                        missingPositions.Add(position.Name);
                    }
                }

                if (missingPositions.Any())
                {
                    missing.Add($"El partido político {party.Name} ({party.PoliticalPartyAcronym.Value}) no tiene candidatos activos asignados para los siguientes puestos electivos: {string.Join(", ", missingPositions)}.");
                }
            }
            return missing;
        }

        public async Task<bool> ExistActiveElection()
        {
                return await _context.Elections.AnyAsync(x => x.ElectionState == ElectionState.Activa && x.State == true);
        }

        public async Task<bool> ValidElectionDate(ElectionDate electionDate)
        {
                return await Task.FromResult(electionDate.Value >= DateTime.Now.Date);
        }

        public async Task<bool> ValidateElectionState(int electionId, ElectionState expectedState)
        {
                 return await _context.Elections.AnyAsync(x => x.Id == electionId && x.ElectionState == expectedState);
        }


    }
}
