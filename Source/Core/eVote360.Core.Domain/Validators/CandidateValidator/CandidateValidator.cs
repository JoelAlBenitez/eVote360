using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.DomainService.Canditate;

namespace eVote360.Core.Domain.Validators.CandidateValidator
{
    public class CandidateValidator : ICandidateValidator
    {
        private readonly ICandidateDomainService _candidateDomainService;

        public CandidateValidator(ICandidateDomainService candidateDomainService)
        {
            _candidateDomainService = candidateDomainService;
        }

        public async Task<ValidationResult> ValidateCreateAsync(int partyId)
        {
            var hasElection = await _candidateDomainService.IsElectionProcessActive();
            if (hasElection)
                return ValidationResult.Failure(CandidatesError.ActiveElectionExists);

            var partyActive = await _candidateDomainService.IsPoliticalPartyActive(partyId);
            if (!partyActive)
                return ValidationResult.Failure(CandidatesError.PoliticalPartyNotActive);

            return ValidationResult.Success();
        }

        public async Task<ValidationResult> ValidateUpdateAsync(int candidateId, string name, string lastName, int partyId)
        {
            var hasElection = await _candidateDomainService.IsElectionProcessActive();
            if (hasElection)
                return ValidationResult.Failure(CandidatesError.ActiveElectionExists);

            var hasParticipated = await _candidateDomainService.CandidateHasParticipatedInElection(candidateId);
            if (hasParticipated)
                return ValidationResult.Failure(CandidatesError.CandidateHasParticipatedElection);

            var nameExists = await _candidateDomainService.CandidateNameExistsInParty(name, lastName, partyId, candidateId);
            if (nameExists)
                return ValidationResult.Failure(CandidatesError.NameAlreadyExists);

            return ValidationResult.Success();
        }

        public async Task<ValidationResult> ValidateChangeStateAsync(int candidateId)
        {
            var hasElection = await _candidateDomainService.IsElectionProcessActive();
            if (hasElection)
                return ValidationResult.Failure(CandidatesError.ActiveElectionExists);

            var hasPosition = await _candidateDomainService.CandidateHasPositionAssigned(candidateId);
            if (hasPosition)
                return ValidationResult.Failure(CandidatesError.CandidateAssignedToPosition);

            return ValidationResult.Success();
        }
    }
}