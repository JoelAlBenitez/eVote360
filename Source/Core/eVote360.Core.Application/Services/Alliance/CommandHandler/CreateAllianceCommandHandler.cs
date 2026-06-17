using eVote360.Core.Application.Alliances.DTOs;
using eVote360.Core.Application.Contracts.Alliance.Commands;
using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalAlliences;
using eVote360.Core.Domain.Entities.PoliticalAlliances;
using eVote360.Core.Domain.Validators.PoliticalAlliancesValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Services.Alliance.CommandHandler
{
    public class CreateAllianceCommandHandler : ICreateAllianceCommand
    {
        private readonly IPoliticalAllienceRepository _allianceRepository;
        private readonly IAllianceValidator _allianceValidator;

        public CreateAllianceCommandHandler(IPoliticalAllienceRepository allianceRepository, IAllianceValidator allianceValidator)
        {
            _allianceRepository = allianceRepository;
            _allianceValidator = allianceValidator;
        }

        public async Task<ValidationResult<AllianceDto>> ExecuteAsync(CreateAllianceRequestDto request, int requestingPartyId, int authenticatedUserId)
        {
            var errors = new List<Error>();
            try
            {
                var validation = await _allianceValidator.ValidateCreateRequestAsync(requestingPartyId, request.ReceivingPartyId);
                if (!validation.IsValid)
                {
                    return ValidationResult<AllianceDto>.Failure(validation.errors.ToList());
                }

                var alliance = new PoliticalAlliances
                {
                    RequestingPartyId = requestingPartyId,
                    ReceivingPartyId = request.ReceivingPartyId,
                    Status = AllianceStatus.Pendiente,
                    RequestDate = DateTimeOffset.Now,
                    CreateAt = DateTimeOffset.Now,
                    CreateUserId = authenticatedUserId
                };

                var created = await _allianceRepository.CreateEntiteAsync(alliance);
                if (!created)
                {
                    errors.Add(new Error("Error en la creación", "No se pudo crear la solicitud de alianza."));
                    return ValidationResult<AllianceDto>.Failure(errors.ToArray());
                }

                var dto = new AllianceDto
                {
                    Id = alliance.Id,
                    RequestingPartyId = alliance.RequestingPartyId,
                    ReceivingPartyId = alliance.ReceivingPartyId,
                    Status = alliance.Status,
                    RequestDate = alliance.RequestDate.DateTime,
                    AcceptedDate = alliance.ResponseDate?.DateTime
                };

                return ValidationResult<AllianceDto>.Success(dto);
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Error inesperado", ex.Message));
                return ValidationResult<AllianceDto>.Failure(errors.ToArray());
            }
        }
    }
}
