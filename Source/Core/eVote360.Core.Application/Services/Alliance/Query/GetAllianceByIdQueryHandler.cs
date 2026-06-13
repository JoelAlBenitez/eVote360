using eVote360.Core.Application.Alliances.DTOs;
using eVote360.Core.Application.Contracts.Alliance.Query;
using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalAlliences;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Services.Alliance.QueryHandler
{
    public class GetAllianceByIdQueryHandler : IGetAllianceByIdQuery
    {
        private readonly IPoliticalAllienceRepository _allianceRepository;

        public GetAllianceByIdQueryHandler(IPoliticalAllienceRepository allianceRepository)
        {
            _allianceRepository = allianceRepository;
        }

        public async Task<ValidationResult<AllianceDto>> ExecuteAsync(int id)
        {
            var errors = new List<Error>();
            try
            {
                var alliance = await _allianceRepository.GetByIdEntitie(id);
                if (alliance == null)
                {
                    errors.Add(AllianceErrors.RequestNotFound);
                    return ValidationResult<AllianceDto>.Failure(errors);
                }

                var dto = new AllianceDto
                {
                    Id = alliance.Id,
                    RequestingPartyId = alliance.RequestingPartyId,
                    ReceivingPartyId = alliance.ReceivingPartyId,
                    Status = alliance.Status,
                    RequestDate = alliance.RequestDate.DateTime,
                    AcceptedDate = alliance.ResponseDate?.DateTime,
                    // TODO: Poblar nombres y siglas cuando el módulo de partidos esté disponible
                    RequestingPartyName = "Partido Solicitante", 
                    RequestingPartySiglas = "PS",
                    ReceivingPartyName = "Partido Receptor",
                    ReceivingPartySiglas = "PR"
                };

                return ValidationResult<AllianceDto>.Success(dto);
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Error al consultar", ex.Message));
                return ValidationResult<AllianceDto>.Failure(errors);
            }
        }
    }
}
