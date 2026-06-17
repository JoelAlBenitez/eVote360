using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.DomainService.ElectivePosition;
using eVote360.Core.Domain.Contracts.ServiceValidates.CandidateAssignment;
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
        private readonly ICandidateAssignmentDomainService _candidateAssignmentDomainService;
        private List<Error> _errors = new List<Error>();
        
       

        public ElectivePositionsValidator(
            IElectivePositionValidate electivePositionDomainService,
            IElectionDomainService electionDomainService,
            IVotesValidate votesValidate,
            ICandidateAssignmentDomainService candidateAssignmentDomainService
            )
        {
            _electivePositionDomainService = electivePositionDomainService;
            _electionDomainService = electionDomainService;
            _votesValidate = votesValidate;
            _candidateAssignmentDomainService = candidateAssignmentDomainService;
        }
      
        private Error ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)
                || name.Trim().Length > 30 ||
                !Regex.IsMatch(name.Trim(), @"^[\p{L}\s]+$"
                )) return ElectivePosictionsError.NameInvalid;

            return null!;
        }

       private Error ValidateDescription(string description)
       {
            if (string.IsNullOrWhiteSpace(description) ||
                description.Trim().Length > 100
                ||
                !Regex.IsMatch(description.Trim(), @"^[\p{L}\s]+$")
                ) return ElectivePosictionsError.DescriptionInvalid;

            return null!;
        }

        public async Task<ValidationResult> ValidateCreateElectivePositions(ElectivePositions electivePositions)
        {
            var errors = new List<Error>();
            var exitsActiveElection = await _electionDomainService.ExistActiveElection();

            if (exitsActiveElection)
            {
                errors.Add(ElectionError.ElectionActive);
                return ValidationResult.Failure(errors);
            }

            if (electivePositions == null)
            {
                errors.Add(ElectivePosictionsError.DataInvalid);
                return ValidationResult.Failure(errors);
            }

            var validate = await _electivePositionDomainService.ExistElectivePositionByName(electivePositions!.Name.Trim());
            if (validate) errors.Add(ElectivePosictionsError.ExistElectivePosictions);

            var vName = ValidateName(electivePositions.Name);
            if (vName != null) errors.Add(vName);
              
            var vDescription = ValidateDescription(electivePositions.Description);
            if(vDescription != null) errors.Add(vDescription);

            if (!electivePositions.State) errors.Add(ElectivePosictionsError.StateNotValid);

            return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success();
        }

        public async Task<ValidationResult> ValidateUpdateElectivePosition(ElectivePositions electivePositions)
        {
            var errors = new List<Error>();
            var exitsActiveElection = await _electionDomainService.ExistActiveElection();

            if (exitsActiveElection)
            {
                errors.Add(ElectionError.ElectionActive);
                return ValidationResult.Failure(errors);
            }

            if (electivePositions == null)
            {
                errors.Add(ElectivePosictionsError.DataInvalid);
                return ValidationResult.Failure(errors);
            }

            var validate = await  _votesValidate.ElectivePositionUsedInElections(electivePositions!.Id);
            if (validate) errors.Add(ElectivePosictionsError.NameCannotChange);

            var exitsElectiveP = await _electivePositionDomainService.ExistById(electivePositions.Id);
            if (!exitsElectiveP) errors.Add(ElectivePosictionsError.NonExistentElectivePosition);

            var validateName = await _electivePositionDomainService.ExistsAnotherElectivePositionWithName(electivePositions.Id, electivePositions.Name);
            if (validateName) errors.Add(ElectivePosictionsError.ExistsAnotherElectivePositionWithName);

            var vName = ValidateName(electivePositions.Name);
            if (vName != null) errors.Add(vName);

            var vDescription = ValidateDescription(electivePositions.Description);
            if (vDescription != null) errors.Add(vDescription);

            return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success();
        }

        public async Task<ValidationResult> ValidateActiveElectivePostions(int Id, string name)
        {
            var errors = new List<Error>();
            var exitsActiveElection = await _electionDomainService.ExistActiveElection();

            if (exitsActiveElection)
            {
                errors.Add(ElectionError.ElectionActive);
                return ValidationResult.Failure(errors);
            }

            var exitsElectiveP = await _electivePositionDomainService.ExistElectivePositionByName(name.Trim());
            if (!exitsElectiveP) errors.Add(ElectivePosictionsError.NonExistentElectivePosition);

            var validate = await _electivePositionDomainService.ExistElectivePositionByState(Id, name, true);
            if (validate) errors.Add(ElectivePosictionsError.ActivedElectivePosiction);

            return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success();
        }   

        public async Task<ValidationResult> ValidateDesactiveElectivePositions(int Id, string name)
        {
            var errors = new List<Error>();
            var exitsActiveElection = await _electionDomainService.ExistActiveElection();

            if (exitsActiveElection)
            {
                errors.Add(ElectionError.ElectionActive);
                return ValidationResult.Failure(errors);
            }
            var validate = await _electivePositionDomainService.ExistElectivePositionByState(Id, name, false);
            if (validate) errors.Add(ElectivePosictionsError.DesactiveElectivePosiction);

            var electivePositionHasCandidates = await _candidateAssignmentDomainService.ElectivePositionHasAssociatedByCandidates(Id);
            if (electivePositionHasCandidates) errors.Add(ElectivePosictionsError.ElectivePosictionHasAssociatedByCandidates);
      
            return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success();
        }
    }
}
