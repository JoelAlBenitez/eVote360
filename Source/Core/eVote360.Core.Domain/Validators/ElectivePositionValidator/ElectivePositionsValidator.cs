using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.DomainService.ElectivePosition;
using eVote360.Core.Domain.Contracts.ServiceValidates.Election;
using eVote360.Core.Domain.Contracts.ServiceValidates.Elector.Votes;
using eVote360.Core.Domain.Entities.ElectivePosition;
using System.Text.RegularExpressions;

namespace eVote360.Core.Domain.Validators.ElectivePositionValidator
{
    public class ElectivePositionsValidator : IElectivePositionsValidator
    {
        private readonly IElectivePositionValidate _electivePositionDomainService;
        private readonly IElectionDomainService _electionDomainService;
        private readonly IVotesValidate _votesValidate;
        private List<Error> _errors = new List<Error>();
        
       

        public ElectivePositionsValidator(
            IElectivePositionValidate electivePositionDomainService,
            IElectionDomainService electionDomainService,
            IVotesValidate votesValidate
            )
        {
            _electivePositionDomainService = electivePositionDomainService;
            _electionDomainService = electionDomainService;
            _votesValidate = votesValidate;
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

            var exitsActiveElection = await _electionDomainService.ExistActiveElection();

            if (exitsActiveElection)
            {
                _errors.Add(ElectionError.ElectionActive);
                return ValidationResult.Failure(_errors);
            }

            if (electivePositions == null) _errors.Add(ElectivePosictionsError.DataInvalid);

            var validate = await _electivePositionDomainService.ExistElectivePositionByName(electivePositions!.Name.Trim());
            if (validate) _errors.Add(ElectivePosictionsError.ExistElectivePosictions);

            var vName = ValidateName(electivePositions.Name);
            if (vName != null) _errors.Add(vName);
              
            var vDescription = ValidateDescription(electivePositions.Description);
            if(vDescription != null) _errors.Add(vDescription);

            if (!electivePositions.State) _errors.Add(ElectivePosictionsError.StateNotValid);

            return _errors.Any() ? ValidationResult.Failure(_errors) : ValidationResult.Success();
        }

        public async Task<ValidationResult> ValidateUpdateElectivePosition(ElectivePositions electivePositions)
        {

            var exitsActiveElection = await _electionDomainService.ExistActiveElection();

            if (exitsActiveElection)
            {
                _errors.Add(ElectionError.ElectionActive);
                return ValidationResult.Failure(_errors);
            }
            if (electivePositions == null) _errors.Add(ElectivePosictionsError.DataInvalid);

            var validate = await  _votesValidate.ElectivePositionUsedInElections(electivePositions!.Id);
            if (validate) _errors.Add(ElectivePosictionsError.NameCannotChange);

            var validateName = await _electivePositionDomainService.ExistsAnotherElectivePositionWithName(electivePositions.Id, electivePositions.Name);
            if (validateName) _errors.Add(ElectivePosictionsError.ExistsAnotherElectivePositionWithName);

            var exitsElectiveP = await _electivePositionDomainService.ExistElectivePositionByName(electivePositions.Name.Trim());
            if (!exitsElectiveP) _errors.Add(ElectivePosictionsError.NonExistentElectivePosition);

            var vName = ValidateName(electivePositions.Name);
            if (vName != null) _errors.Add(vName);

            var vDescription = ValidateDescription(electivePositions.Description);
            if (vDescription != null) _errors.Add(vDescription);

            return _errors.Any() ? ValidationResult.Failure(_errors) : ValidationResult.Success();
        }

        public async Task<ValidationResult> ValidateActiveElectivePostions(int Id, string name)
        {
            var exitsActiveElection = await _electionDomainService.ExistActiveElection();

            if (exitsActiveElection)
            {
                _errors.Add(ElectionError.ElectionActive);
                return ValidationResult.Failure(_errors);
            }

            var exitsElectiveP = await _electivePositionDomainService.ExistElectivePositionByName(name.Trim());
            if (!exitsElectiveP) _errors.Add(ElectivePosictionsError.NonExistentElectivePosition);

            var validate = await _electivePositionDomainService.ExistElectivePositionByState(Id, name, true);
            if (validate) _errors.Add(ElectivePosictionsError.ActivedElectivePosiction);

            return _errors.Any() ? ValidationResult.Failure(_errors) : ValidationResult.Success();
        }   

        public async Task<ValidationResult> ValidateDesactiveElectivePositions(int Id, string name)
        {

            var exitsActiveElection = await _electionDomainService.ExistActiveElection();

            if (exitsActiveElection)
            {
                _errors.Add(ElectionError.ElectionActive);
                return ValidationResult.Failure(_errors);
            }
            var validate = await _electivePositionDomainService.ExistElectivePositionByState(Id, name, false);
            if (validate) _errors.Add(ElectivePosictionsError.DesactiveElectivePosiction);

            var electivePositionHasCandidates = await _electivePositionDomainService.ElectivePositionHasAssociatedByCandidates(Id);
            if (electivePositionHasCandidates) _errors.Add(ElectivePosictionsError.ElectivePosictionHasAssociatedByCandidates);
      
            return _errors.Any() ? ValidationResult.Failure(_errors) : ValidationResult.Success();
        }
    }
}
