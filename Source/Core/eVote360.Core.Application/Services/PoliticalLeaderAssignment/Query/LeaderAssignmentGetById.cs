using eVote360.Core.Application.Contracts.PoliticalLeaderAssignment.Query;
using eVote360.Core.Application.DTOs.PoliticalLeaderAssignment;
using eVote360.Core.Domain.Commom.BaseEntity;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalAssignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Services.PoliticalLeaderAssignment.Query
{
    public sealed class LeaderAssignmentGetById : ILeaderAssignmentGetByIdQuery
    {
        private readonly IPoliticalAssignmentRepository _repository;

        public LeaderAssignmentGetById(IPoliticalAssignmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult<LeaderAssignmentDto>> ExecuteAsync(int Id)
        {
            var entity = await _repository.GetByIdEntitie(Id);

            if (entity == null) {
                var errors = new List<Error> { new Error("ASIG ERROR", "La Asignacion no fue encontrada") };

                return ValidationResult<LeaderAssignmentDto>.Failure(errors);
            }

            var dto = new LeaderAssignmentDto
            {
                Id = entity.Id,
                Name = entity.Name,
                State = entity.State,
                PoliticalLeaderId = entity.PoliticalLeaderId,
                PoliticalPartyId = entity.PoliticalPartyId,
                CreateAt = entity.CreateAt,
                CreateUserId = entity.CreateUserId,
                UpdateUserId = entity.UpdateUserId,
                UpdateAt = entity.UpdateAt,
                PoliticalAssignmentDate = entity.PolitcalAssignmentDate
            };
            return ValidationResult<LeaderAssignmentDto>.Success(dto);
        }
    }
}
