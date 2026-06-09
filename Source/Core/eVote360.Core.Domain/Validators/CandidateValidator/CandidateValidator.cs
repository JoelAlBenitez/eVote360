using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.DomainService.Canditate;
using eVote360.Core.Domain.Entities.Candidate;


namespace eVote360.Core.Domain.Validators.CandidateValidator
{
    public class CandidateValidator : ICandidateValidator
    {
        private readonly ICandidateDomainService _CandidateDomainService;
        public CandidateValidator(ICandidateDomainService candidateDomainService) {


            _CandidateDomainService = candidateDomainService;



        }
        public async Task<ValidationResult> ValidateChangeStateAsync(Candidates candidate)
        {


          

            var hasElectionActive = await _CandidateDomainService.IsElectionProcessActive();

            if (hasElectionActive)
            {
                return ValidationResult.Failure(CandidatesError.ActiveElectionExists);
            }

            var HasPositionAssigned = await _CandidateDomainService.CandidateHasPositionAssigned(candidate.Id);

            if (HasPositionAssigned)
            {
                return ValidationResult.Failure(CandidatesError.CandidateAssignedToPosition);
            }

            return ValidationResult.Success();
        }

        public async Task<ValidationResult> ValidateCreateAsync(Candidates candidate)
        {
            
            var hasElection = await _CandidateDomainService.IsElectionProcessActive();

            if (hasElection)
            {
                return ValidationResult.Failure(CandidatesError.ActiveElectionExists);
            }

            var PoliticalPartyIsActive = await _CandidateDomainService.IsPoliticalPartyActive(candidate.PoliticalPartyId);

            if (!PoliticalPartyIsActive)
            {
                return ValidationResult.Failure(CandidatesError.PoliticalPartyNotActive); 
            }
            return ValidationResult.Success();

        }

        public async Task<ValidationResult> ValidateUpdateAsync(Candidates candidate)
        {
     
            var HasElectionActive = await _CandidateDomainService.IsElectionProcessActive();

            if (HasElectionActive == true)
            {
                return ValidationResult.Failure(CandidatesError.ActiveElectionExists);
            }


            if (candidate.HasParticipatedInElection)
            {
                return ValidationResult.Failure(CandidatesError.CandidateHasParticipatedElection);

            }

            return ValidationResult.Success();


        }
    }
}
