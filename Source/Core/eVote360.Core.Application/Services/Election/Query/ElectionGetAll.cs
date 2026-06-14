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

        public async Task<IReadOnlyCollection<ElectionDto>> ExecuteAsync()
        {
            var entities = await _repository.GetAllElectionsAsync();

            return entities.Select(e => new ElectionDto
            {
                Id = e.Id,
                Name = e.Name,
                State = e.State,

                ElectionDate = e.ElectionDate.Value,

                ElectionState = e.ElectionState,

                CreateAt = e.CreateAt?.Date ?? DateTime.MinValue,
                UpdateAt = e.UpdateAt?.Date ?? DateTime.MinValue,
                CreateUserId = e.CreateUserId ?? 0,
                UpdateUserId = e.UpdateUserId ?? 0,
            }).ToList().AsReadOnly();
        }
    }
}
