using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.DomainService.ElectivePosition;
using eVote360.Core.Domain.Entities.ElectivePosition;
using System.Text.RegularExpressions;

namespace eVote360.Core.Domain.Validators.ElectivePositionValidator
{
    public class ElectivePositionsValidator : IElectivePositionsValidator
    {
        private readonly IElectivePositionDomainService _electivePositionDomainService;
        private List<Error> errrors = new List<Error>();
        
        //agregar llamanda de domain service de elecciones 

        public ElectivePositionsValidator(IElectivePositionDomainService electivePositionDomainService)
        {
            _electivePositionDomainService = electivePositionDomainService;
        }
      
        private Error ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)
                || name.Trim().Length > 30 ||
                !Regex.IsMatch("^[a-zA-Z\\s]+$", name.Trim()
                )) return ElectivePosictionsError.NameInvalid;

            return null!;
        }

       private Error ValidateDescription(string description)
       {
            if (string.IsNullOrWhiteSpace(description) ||
                description.Trim().Length > 100
                ||
                !Regex.IsMatch("^[a-zA-Z\\s]+$", description.Trim())
                ) return ElectivePosictionsError.DescriptionInvalid;

            return null!;
        }

        public async Task<ValidationResult> ValidateCreateElectivePositions(ElectivePositions electivePositions)
        {

            if (electivePositions == null) errrors.Add(ElectivePosictionsError.DataInvalid);

            var validate = await _electivePositionDomainService.ExistElectivePositionByName(electivePositions!.Name.Trim());
            if (validate) errrors.Add(ElectivePosictionsError.ExistElectivePosictions);

            var vName = ValidateName(electivePositions.Name);
            if (vName != null) errrors.Add(vName);
              
            var vDescription = ValidateDescription(electivePositions.Description);
            if(vDescription != null) errrors.Add(vDescription);

            if (!electivePositions.State) errrors.Add(ElectivePosictionsError.StateNotValid);

            return errrors.Any() ? ValidationResult.Failure(errrors) : ValidationResult.Success();
        }

        public async Task<ValidationResult> ValidateUpdateElectivePosition(ElectivePositions electivePositions)
        {

            //aregagr metodo de elecctiones activas llamando de domains service de eleciones

            if (electivePositions == null) errrors.Add(ElectivePosictionsError.DataInvalid);

            var validate = await _electivePositionDomainService.ElectivePositionUsedInElections(electivePositions!.Id, electivePositions.Name);
            if (validate) errrors.Add(ElectivePosictionsError.NameCannotChange);

            var validateName = await _electivePositionDomainService.ExistsAnotherElectivePositionWithName(electivePositions.Id, electivePositions.Name);
            if (validateName) errrors.Add(ElectivePosictionsError.ExistsAnotherElectivePositionWithName);

            var exitsElectiveP = await _electivePositionDomainService.ExistElectivePositionByName(electivePositions.Name.Trim());
            if (!exitsElectiveP) errrors.Add(ElectivePosictionsError.NonExistentElectivePosition);

            var vName = ValidateName(electivePositions.Name);
            if (vName != null) errrors.Add(vName);

            var vDescription = ValidateDescription(electivePositions.Description);
            if (vDescription != null) errrors.Add(vDescription);

            return errrors.Any() ? ValidationResult.Failure(errrors) : ValidationResult.Success();
        }

        public async Task<ValidationResult> ValidateActiveElectivePostions(int Id, string name)
        {

            var exitsElectiveP = await _electivePositionDomainService.ExistElectivePositionByName(name.Trim());
            if (!exitsElectiveP) errrors.Add(ElectivePosictionsError.NonExistentElectivePosition);

            var validate = await _electivePositionDomainService.ExistElectivePositionByState(Id, name, true);
            if (validate) errrors.Add(ElectivePosictionsError.ActivedElectivePosiction);

            return errrors.Any() ? ValidationResult.Failure(errrors) : ValidationResult.Success();
        }   

        public async Task<ValidationResult> ValidateDesactiveElectivePositions(int Id, string name)
        {
           
            var validate = await _electivePositionDomainService.ExistElectivePositionByState(Id, name, false);
            if (validate) errrors.Add(ElectivePosictionsError.DesactiveElectivePosiction);

            var electivePositionHasCandidates = await _electivePositionDomainService.ElectivePositionHasAssociatedByCandidates(Id, name);
            if (electivePositionHasCandidates) errrors.Add(ElectivePosictionsError.ElectivePosictionHasAssociatedByCandidates);
      
            return errrors.Any() ? ValidationResult.Failure(errrors) : ValidationResult.Success();
        }
    }
}
