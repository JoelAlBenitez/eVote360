
using eVote360.Core.Application.Contracts.ElectivePosictions.Commands;
using eVote360.Core.Application.DTOs.ElectivePositions;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.ElectivePosition;
using eVote360.Core.Domain.Validators.ElectivePositionValidator;

namespace eVote360.Core.Application.Services.ElectivePosiction.CommandHandler
{
    public class ElectivePosictionsAlterState : IElectivePosictionsAlterState
    {

        private readonly IElectivePositionsRepository _electivePositionsRepository;
        private readonly IElectivePositionsValidator _electivePositionsValidator;
        private List<Error> _errors = new List<Error>();

        public ElectivePosictionsAlterState(IElectivePositionsRepository electivePositionsRepository,
                IElectivePositionsValidator electivePositionsValidator
            )
        {
            _electivePositionsRepository = electivePositionsRepository;
            _electivePositionsValidator = electivePositionsValidator;
        }

        public async Task<ValidationResult> AlterState(ElectivePosictionsDesactiveOrActive dto)
        { 
            try
            {
                //validate de autorization

                var elective = await _electivePositionsRepository.GetByIdEntitie(dto.IdElectivePosition);

                if (elective.State)
                {
                    var validate = await _electivePositionsValidator.ValidateDesactiveElectivePositions(elective.Id, elective.Name);
                    if (!validate.IsValid) return validate;

                    var alterState = await _electivePositionsRepository.AlterState(elective.Id, !elective.State);
                    if (!alterState)
                    {
                        _errors.Add(new Error("Ha ocurrido un error", "Ha ocurrido un error inesperado en la alteración del registro. "));
                        return ValidationResult.Failure(_errors);
                    }
                }
                else
                {
                    var validate = await _electivePositionsValidator.ValidateActiveElectivePostions(elective.Id, elective.Name);
                    if (!validate.IsValid) return validate;

                    var alterState = await _electivePositionsRepository.AlterState(elective.Id, !elective.State);
                    if (!alterState)
                    {
                        _errors.Add(new Error("Ha ocurrido un error", "Ha ocurrido un error inesperado en la alteración del registro. "));
                        return ValidationResult.Failure(_errors);
                    }
                }
                return ValidationResult.Success();
            }
            catch (Exception ex)
            {
                _errors.Add(new Error("Ha ocurrido un error ", ex.Message));
                return ValidationResult.Failure(_errors);

            }
        }
    }
}
