
using eVote360.Core.Application.Contracts.Authentication.Command;
using eVote360.Core.Application.Contracts.ElectivePosictions.Commands;
using eVote360.Core.Application.DTOs.ElectivePositions;
using eVote360.Core.Domain.Commom;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.ElectivePosition;
using eVote360.Core.Domain.Entities.ElectivePosition;
using eVote360.Core.Domain.Validators.ElectivePositionValidator;

namespace eVote360.Core.Application.Services.ElectivePosiction.CommandHandler
{
    public class ElectivePosictionsUpdate : IElectivePosictionsUpdateCommand
    {
        private readonly IElectivePositionsRepository _electivePositionsRepository;
        private readonly IElectivePositionsValidator _electivePositionsValidator;
        private readonly ISessionUser _sessionUser;
        public List<Error> _errors = new List<Error>();

        public ElectivePosictionsUpdate(IElectivePositionsRepository electivePositionsRepository,
            IElectivePositionsValidator electivePositionsValidator,
            ISessionUser sessionUser
            )
        {
            _electivePositionsRepository = electivePositionsRepository;
            _electivePositionsValidator = electivePositionsValidator;
            _sessionUser = sessionUser;

        }

        public async Task<ValidationResult> UpdateAsync(ElectivePosictionsDto dto)
        {
            try
            {
                

                var eleccion = new ElectivePositions()
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Description = dto.Descriptions,
                    State = dto.State,
                    UpdateUserId = _sessionUser.GetUserId(),
                    UpdateAt = DateTimeOffset.Now
                };

                var validate = await _electivePositionsValidator.ValidateUpdateElectivePosition(eleccion);
                if (!validate.IsValid) return validate;

                var update = await _electivePositionsRepository.UpdateEntitieAsync(eleccion);
                _errors.Add(new Error("Error inesperado", "Ha ocurrido un error inesperado en la modificación del registro, favor intente de nuevo mas tarde."));
                if (!update) return ValidationResult.Failure(_errors);

                return ValidationResult.Success();

            }
            catch (Exception ex)
            {
                _errors.Add(new Error("Ha ocurrido un error en la comunicación", ex.Message));
                return ValidationResult.Failure(_errors);
            }
        }
    }
}