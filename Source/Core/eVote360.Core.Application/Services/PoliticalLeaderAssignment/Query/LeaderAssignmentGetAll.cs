using eVote360.Core.Application.Contracts.PoliticalLeaderAssignment.Query;
using eVote360.Core.Application.DTOs.PoliticalLeaderAssignment;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalAssignment;
using System.Linq;

namespace eVote360.Core.Application.Services.PoliticalLeaderAssignment.Query
{
    public sealed class LeaderAssignmentGetAll : ILeaderAssignmentGetAllQuery
    {
        private readonly IPoliticalAssignmentRepository _repository;

        public LeaderAssignmentGetAll(IPoliticalAssignmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<LeaderAssignmentDto>> ExecuteAsync()
        {
            var entities = await _repository.GetAllAsync();

            return entities.Select(a => new LeaderAssignmentDto
            {
                Id = a.Id,
                Name = a.Name,
                PoliticalLeaderId = a.PoliticalLeaderId,
                PoliticalPartyId = a.PoliticalPartyId,
                CreateAt = a.CreateAt,
                CreateUserId = a.CreateUserId,
                UpdateUserId = a.UpdateUserId,
                UpdateAt = a.UpdateAt,
                State = a.State,
                PoliticalAssignmentDate = a.PolitcalAssignmentDate

            }).ToList().AsReadOnly();
        }
    }
}
