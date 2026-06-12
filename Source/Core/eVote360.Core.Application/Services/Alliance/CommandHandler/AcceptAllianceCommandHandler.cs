using eVote360.Core.Application.Alliances.DTOs;
using eVote360.Core.Application.Contracts.Alliance.Commands;
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
    public class AcceptAllianceCommandHandler : IAcceptAllianceCommand
    {
        private readonly IPoliticalAllienceRepository _allianceRepository;
        private readonly IAllianceValidator _allianceValidator;

        public AcceptAllianceCommandHandler(IPoliticalAllienceRepository allianceRepository, IAllianceValidator allianceValidator)
        {
            _allianceRepository = allianceRepository;
            _allianceValidator = allianceValidator;
        }

        public async Task<ValidationResult<AllianceDto>> ExecuteAsync(int allianceId, int authenticatedPartyId)
        {
            var errors = new List<Error>();
            try
            {
                var alliance = await _allianceRepository.GetByIdEntitie(allianceId);
                
                var validation = await _allianceValidator.ValidateAcceptRequestAsync(alliance, authenticatedPartyId);
                if (!validation.IsValid)
                {
                    return ValidationResult<AllianceDto>.Failure(new List<Error>(), validation.errors.ToArray());
                }

                // DDD: Usando el método de la entidad
                alliance!.Accept();

                var updated = await _allianceRepository.UpdateEntitieAsync(alliance);
                if (!updated)
                {
                    errors.Add(new Error("Error al actualizar", "No se pudo aceptar la solicitud de alianza."));
                    return ValidationResult<AllianceDto>.Failure(new List<Error>(), errors.ToArray());
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
                return ValidationResult<AllianceDto>.Failure(new List<Error>(), errors.ToArray());
            }
        }
    }
}
