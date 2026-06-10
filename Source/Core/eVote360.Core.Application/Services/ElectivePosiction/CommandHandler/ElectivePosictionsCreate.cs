using eVote360.Core.Application.Contracts.ElectivePosictions.Commands;
using eVote360.Core.Application.DTOs.ElectivePositions;
using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.ElectivePosition;
using eVote360.Core.Domain.Entities.ElectivePosition;
using eVote360.Core.Domain.Validators.ElectivePositionValidator;

namespace eVote360.Core.Application.Services.ElectivePosiction.CommandHandler
{
    public class ElectivePosictionsCreate : IElectivePosictionsCreateCommand
    {

        private readonly IElectivePositionsRepository _electivePositionsRepository;
        private readonly IElectivePositionsValidator _electivePositionsValidator;
        public List<Error> _errors = new List<Error>();

        public ElectivePosictionsCreate(IElectivePositionsRepository electivePositionsRepository,
            IElectivePositionsValidator electivePositionsValidator)
        {
            _electivePositionsRepository = electivePositionsRepository;
            _electivePositionsValidator = electivePositionsValidator;
        }

        public async Task<ValidationResult> CreateAsync(ElectivePosictionsDto dto)
        {
            try
            {
                var electiveP = new ElectivePositions()
                {
                    Name = dto.Name.Trim().ToLower(),
                    State = dto.State,
                    CreateAt = DateTimeOffset.Now,
                    CreateUserId = 0, //modificar para que este valor venga de la cookie
                    Description = dto.Descriptions,
                };

                var validate = await _electivePositionsValidator.ValidateCreateElectivePositions(electiveP);
                if (!validate.IsValid) return validate;

                var create = await _electivePositionsRepository.CreateEntiteAsync(electiveP);

                if (!create)
                {
                    _errors.Add(new Error("Ha ocurrido un error inesperado", "Ha ocurrido un fallo a interntar crear la posición electiva, intente lo de nuevo más tarde."));
                    return ValidationResult.Failure(_errors);
                }

                return ValidationResult.Success();

            }
            catch (Exception ex)
            {
                _errors.Add(new Error("Error en la creación de la posición electiva", ex.Message));
                return ValidationResult.Failure(_errors);
            }
        }
    }
}
