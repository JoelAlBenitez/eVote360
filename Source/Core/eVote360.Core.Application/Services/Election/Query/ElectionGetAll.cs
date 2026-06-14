using eVote360.Core.Application.Contracts.Election.Query;
using eVote360.Core.Application.DTOs.Election;
using eVote360.Core.Domain.Contracts.Repositories.ElectionRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Services.Election.Query
{
    public sealed class ElectionGetAll : IElectionGetAllQuery
    {
        private readonly IElectionRepository _repository;

        public ElectionGetAll(IElectionRepository repository) 
        {
        
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<ElectionResumDto>> ExecuteAsync()
        {
            var entities = await _repository.GetAllElectionsAsync();

            return entities.Select(e => new ElectionResumDto
            {
                
                Id = e.Id,
                Name = e.NameElection,
                ElectionDate = e.DateRealized,
                ElectionState = e.State,
                NumberCandidactesParticipating = e.NumberElectivePositionsParticipating,
                NumberCitizenParticipating = e.NumberCitizenParticipating,
                NumberParticipatingMatches = e.NumberParticipatingMatches
              
            }).ToList().AsReadOnly();
        }
    }
}
