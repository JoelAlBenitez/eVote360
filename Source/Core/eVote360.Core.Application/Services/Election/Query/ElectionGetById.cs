using eVote360.Core.Application.Contracts.Election.Query;
using eVote360.Core.Application.DTOs.Election;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.ElectionRepository;



namespace eVote360.Core.Application.Services.Election.Query
{
    public sealed class ElectionGetById : IElectionGetByIdQuery
    {
        private readonly IElectionRepository _repository;
    

    public ElectionGetById(IElectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult<ElectionDto>> ExecuteAsync(int id)
        {
            var electionEntity = await _repository.GetByIdEntitie(id);

            if(electionEntity == null)
            {
                return (ValidationResult<ElectionDto>)ValidationResult<ElectionDto>.Success(null!);
            }

            var dto = new ElectionDto
            {
                Id = electionEntity.Id,
                Name = electionEntity.Name,
                State = electionEntity.State,

                ElectionDate = electionEntity.ElectionDate.Value,
                ElectionState = electionEntity.ElectionState,

                CreateAt = electionEntity.CreateAt?.DateTime ?? DateTime.MinValue,
                UpdateAt = electionEntity.UpdateAt?.DateTime ?? DateTime.MinValue,
                CreateUserId = electionEntity.CreateUserId ?? 0,
                UpdateUserId = electionEntity.UpdateUserId ?? 0
            };
            return ValidationResult<ElectionDto>.Success(dto);
        }
    }
}
