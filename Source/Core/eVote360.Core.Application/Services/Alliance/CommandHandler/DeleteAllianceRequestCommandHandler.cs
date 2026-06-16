using eVote360.Core.Application.Contracts.Alliance.Commands;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalAlliences;
using eVote360.Core.Domain.Validators.PoliticalAlliancesValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Services.Alliance.CommandHandler
{
    public class DeleteAllianceRequestCommandHandler : IDeleteAllianceRequestCommand
    {
        private readonly IPoliticalAllienceRepository _allianceRepository;
        private readonly IAllianceValidator _allianceValidator;

        public DeleteAllianceRequestCommandHandler(IPoliticalAllienceRepository allianceRepository, IAllianceValidator allianceValidator)
        {
            _allianceRepository = allianceRepository;
            _allianceValidator = allianceValidator;
        }

        public async Task<ValidationResult<bool>> ExecuteAsync(int allianceId, int authenticatedPartyId)
        {
            var errors = new List<Error>();
            try
            {
                var alliance = await _allianceRepository.GetByIdEntitie(allianceId);
                
                var validation = await _allianceValidator.ValidateDeleteRequestAsync(alliance, authenticatedPartyId);
                if (!validation.IsValid)
                {
                    return ValidationResult<bool>.Failure(validation.errors.ToList());
                }

                var deleted = await _allianceRepository.DeleteAsync(allianceId);
                if (!deleted)
                {
                    errors.Add(new Error("Error al eliminar", "No se pudo eliminar la solicitud de alianza."));
                    return ValidationResult<bool>.Failure(errors.ToArray());
                }

                return ValidationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Error inesperado", ex.Message));
                return ValidationResult<bool>.Failure(errors.ToArray());
            }
        }
    }
}
