using eVote360.Core.Application.Alliances.DTOs;
using eVote360.Core.Application.Contracts.Alliance.Query;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalAlliences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Services.Alliance.QueryHandler
{
    public class GetActiveAlliancesQueryHandler : IGetActiveAlliancesQuery
    {
        private readonly IPoliticalAllienceRepository _allianceRepository;

        public GetActiveAlliancesQueryHandler(IPoliticalAllienceRepository allianceRepository)
        {
            _allianceRepository = allianceRepository;
        }

        public async Task<ValidationResult<IEnumerable<AllianceDto>>> ExecuteAsync(int authenticatedPartyId)
        {
            var errors = new List<Error>();
            try
            {
                var alliances = await _allianceRepository.GetActiveAlliancesAsync(authenticatedPartyId);
                
                var dtos = alliances.Select(a => new AllianceDto
                {
                    Id = a.Id,
                    RequestingPartyId = a.RequestingPartyId,
                    ReceivingPartyId = a.ReceivingPartyId,
                    Status = a.Status,
                    RequestDate = a.RequestDate.DateTime,
                    AcceptedDate = a.ResponseDate?.DateTime
                });

                return ValidationResult<IEnumerable<AllianceDto>>.Success(dtos);
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Error al consultar", ex.Message));
                return ValidationResult<IEnumerable<AllianceDto>>.Failure(errors.ToArray());
            }
        }
    }
}
