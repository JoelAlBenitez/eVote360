using eVote360.Core.Application.Contracts.Election.Query;
using eVote360.Core.Application.DTOs.Election;
using eVote360.Core.Domain.Contracts.Repositories.ElectionRepository;

namespace eVote360.Core.Application.Services.Election.Query
{
    public sealed  class ElectionGetByYear : IElectionByYearQuery
    {
        private readonly IElectionRepository _repository;

        public ElectionGetByYear(IElectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<ElectionDto>> ExecuteAsync(int year)
        {
           var entities = await _repository.GetElectionsByYearAsync(year);

            return entities.Select(e => new ElectionDto
            {
                Id = e.Id,
                Name = e.Name,
                ElectionDate = e.ElectionDate.Value,
                ElectionState = e.ElectionState,
                State = e.State,
                CreateAt = e.CreateAt,
                CreateUserId = e.CreateUserId,
                UpdateAt = e.UpdateAt,
                UpdateUserId = e.UpdateUserId,
            }).ToList().AsReadOnly();
        }
    }
}
