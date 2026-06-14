using eVote360.Core.Application.Contracts.CandidateAssignment.Commands;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.CandidateAssignment;
using eVote360.Core.Domain.Contracts.Validators.CandidateAssignment;
using CandidateAssignmentEntity = eVote360.Core.Domain.Entities.CandidateAssignment.CandidateAssignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eVote360.Core.Application.DTOs.CandidateAssignment;

namespace eVote360.Core.Application.Services.CandidateAssignment.CommandHandler
{
    public class CreateAssignmentCommandHandler : ICreateAssignmentCommand
    {
        private readonly ICandidateAssignmentRepository _repository;
        private readonly IAssignmentValidator _validator;

        public CreateAssignmentCommandHandler(ICandidateAssignmentRepository repository, IAssignmentValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ValidationResult<CandidateAssignmentDto>> ExecuteAsync(CreateAssignmentRequestDto request, int assigningPartyId, int authenticatedUserId)
        {
            var errors = new List<Error>();
            try
            {
                var validation = await _validator.ValidateCreateAsync(request.CandidateId, request.ElectivePositionId, assigningPartyId);
                if (!validation.IsValid)
                {
                    return ValidationResult<CandidateAssignmentDto>.Failure(validation.errors.ToList());
                }

                var entity = new CandidateAssignmentEntity
                {
                    CandidateId = request.CandidateId,
                    ElectivePositionId = request.ElectivePositionId,
                    AssigningPartyId = assigningPartyId,
                    CreateAt = DateTimeOffset.Now,
                    CreateUserId = authenticatedUserId
                };

                var created = await _repository.CreateEntiteAsync(entity);
                if (!created)
                {
                    errors.Add(new Error("Error en la creación", "No se pudo crear la asignación del candidato."));
                    return ValidationResult<CandidateAssignmentDto>.Failure(errors.ToArray());
                }

                var dto = new CandidateAssignmentDto
                {
                    AssignmentId = entity.Id,
                    AssigningPartyId = entity.AssigningPartyId,
                    CandidateId = entity.CandidateId,
                    ElectivePositionId = entity.ElectivePositionId
                    // Los campos de nombre, foto, tipo se llenan en Infrastructure con joinsss x4
                };

                return ValidationResult<CandidateAssignmentDto>.Success(dto);
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Error inesperado", ex.Message));
                return ValidationResult<CandidateAssignmentDto>.Failure(errors.ToArray());
            }
        }
    }
}
