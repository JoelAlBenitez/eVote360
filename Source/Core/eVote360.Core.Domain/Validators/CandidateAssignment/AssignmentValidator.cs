using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.Candidate;
using eVote360.Core.Domain.Contracts.Repositories.CandidateAssignment;
using eVote360.Core.Domain.Contracts.ServiceValidates.CandidateAssignment;
using eVote360.Core.Domain.Contracts.Validators.CandidateAssignment;
using System.Threading.Tasks;
using System.Linq;

namespace eVote360.Core.Domain.Validators.AssignmentValidator
{
    public class AssignmentValidator : IAssignmentValidator
    {
        private readonly ICandidateAssignmentDomainService _domainService;
        private readonly ICandidateAssignmentRepository _repository;
        private readonly ICandidateRepository _candidateRepository;

        public AssignmentValidator(
            ICandidateAssignmentDomainService domainService,
            ICandidateAssignmentRepository repository,
            ICandidateRepository candidateRepository)
        {
            _domainService = domainService;
            _repository = repository;
            _candidateRepository = candidateRepository;
        }

        public async Task<ValidationResult> ValidateCreateAsync(int candidateId, int electivePositionId, int assigningPartyId)
        {
            if (await _domainService.IsElectionProcessActive())
                return ValidationResult.Failure(AssignmentErrors.ActiveElectionExists);

            if (!await _domainService.IsPartyActive(assigningPartyId))
                return ValidationResult.Failure(AssignmentErrors.PartyNotActive);

            var candidate = await _candidateRepository.GetByIdEntitie(candidateId);
            if (candidate == null)
                return ValidationResult.Failure(AssignmentErrors.CandidateNotFound);

            if (!await _domainService.CandidateIsActive(candidateId))
                return ValidationResult.Failure(AssignmentErrors.CandidateNotActive);

           
            if (!await _domainService.ElectivePositionIsActive(electivePositionId))
                return ValidationResult.Failure(AssignmentErrors.ElectivePositionNotActive);

            if (await _repository.ExistsAssignmentForPositionInParty(electivePositionId, assigningPartyId))
                return ValidationResult.Failure(AssignmentErrors.ElectivePositionAlreadyOccupied);

   
            if (await _repository.ExistsAssignmentForCandidateInParty(candidateId, assigningPartyId))
                return ValidationResult.Failure(AssignmentErrors.CandidateAlreadyAssigned);

         
            int originPartyId = candidate.PoliticalPartyId;
            if (originPartyId == assigningPartyId)
            {
                return ValidationResult.Success();
            }

           
            if (!await _domainService.ExistsActiveAllianceBetweenParties(assigningPartyId, originPartyId))
                 return ValidationResult.Failure(AssignmentErrors.NoActiveAlliance);

           
            if (!await _domainService.CandidateHasAssignmentInOriginParty(candidateId, originPartyId))
                return ValidationResult.Failure(AssignmentErrors.AlliedCandidateHasNoPosition);

        
            var originPosition = await _domainService.GetCandidatePositionInOriginParty(candidateId, originPartyId);
            if (originPosition != electivePositionId)
                return ValidationResult.Failure(AssignmentErrors.AlliedCandidateDifferentPosition);

            return ValidationResult.Success();
        }

        public async Task<ValidationResult> ValidateDeleteAsync(int assignmentId, int assigningPartyId)
        {
            
            if (await _domainService.IsElectionProcessActive())
                return ValidationResult.Failure(AssignmentErrors.ActiveElectionDeleteForbidden);

           
            var assignment = await _repository.GetByIdEntitie(assignmentId);
            if (assignment == null)
                return ValidationResult.Failure(AssignmentErrors.AssignmentNotFound);

            
            if (assignment.AssigningPartyId != assigningPartyId)
                return ValidationResult.Failure(AssignmentErrors.AssignmentNotBelongsToParty);

            return ValidationResult.Success();
        }
    }
}
