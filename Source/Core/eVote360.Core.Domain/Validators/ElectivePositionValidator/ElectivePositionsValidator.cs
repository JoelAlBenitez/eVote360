    using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.DomainService.ElectivePosition;
using System.Text.RegularExpressions;

namespace eVote360.Core.Domain.Validators.ElectivePositionValidator
{
    public class ElectivePositionsValidator : IElectivePositionsValidator
    {
        private readonly IElectivePositionDomainService _electivePositionDomainService;
        
        //agregar llamanda de domain service de elecciones 
        public ElectivePositionsValidator(IElectivePositionDomainService electivePositionDomainService)
        {
            _electivePositionDomainService = electivePositionDomainService;
        }
        
        public async Task<ValidationResult> ValidateElectivePositios(Entities.ElectivePosition.ElectivePositions elective)
        {
            var errorrs = new List<Error>();
            var validations = new[]
            {
                await ValidateNameElective(elective.Name),
                ValidateDescription(elective.Description),
                await ValidateElectivePosictionsHasEleccion(elective.Id, elective.Name),
                await ValidteElectivePosictionHasAssociateCandidates(elective.Id, elective.Name),
                await ValidateChangeStateByActive(elective.Id, elective.Name),
                await ValidateChangeStateByDesactive(elective.Id, elective.Name)
                
            };
            errorrs.AddRange(validations.Where(v => v != null));
            return errorrs.Any() ? ValidationResult.Failure() : ValidationResult.Success();
        }
        private  async Task<Error> ValidateNameElective(string name)
        {
            if (name.Trim().Length > 30 && !Regex.IsMatch("^[a-zA-Z\\s]+$", name.Trim()))
                return ElectivePosictionsError.DataInvalid;
            var validate = await _electivePositionDomainService.ExistElectivePositionByName(name.Trim());
            if (validate) return ElectivePosictionsError.NameInvalid;
            return null!;
        } 
        private Error  ValidateDescription(string description)
        {
            if (description.Trim().Length > 100 && !Regex.IsMatch("^[a-zA-Z\\s]+$", description.Trim()))
                return ElectivePosictionsError.DescriptionInvalid;
            return null!;
        }
        private async Task<Error> ValidateElectivePosictionsHasEleccion(int Id, string name)
        {
            //agregar llamado al domain service de elecciones, obtener estado valido de la eleccion
            var validate = await _electivePositionDomainService.ElectivePositionUsedInElections(Id, name);
            if (validate) return ElectivePosictionsError.ElectivePosictionUsedByEleccionsActive;
            return null!;

        }
        private async Task<Error>  ValidteElectivePosictionHasAssociateCandidates(int Id, string name)
        {
            var validate = await _electivePositionDomainService.ElectivePositionHasAssociatedByCandidates(Id, name);
            if (validate) return ElectivePosictionsError.ElectivePosictionHasAssociatedByCandidates;
            return null!;

        }
        private async Task<Error> ValidateChangeStateByActive(int Id, string name)
        {
            var validate = await _electivePositionDomainService.ExistElectivePositionByState(Id, name, true);
            if (validate) return ElectivePosictionsError.ActivedElectivePosiction;
            return null!;

        }
        private async Task<Error> ValidateChangeStateByDesactive(int Id, string name) {

            var validate = await _electivePositionDomainService.ExistElectivePositionByState(Id, name, false);
            if (validate) return ElectivePosictionsError.DesactiveElectivePosiction;
            return null!;
        }


    }
}
