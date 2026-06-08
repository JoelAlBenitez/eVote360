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


            var errors = new List<Error>();

            var hasElectionActive = await _CandidateDomainService.IsElectionProcessActive();

            if (hasElectionActive)
            {
                errors.Add(CandidatesError.ActiveElectionExists);
            }

            var HasPositionAssigned = await _CandidateDomainService.CandidateHasPositionAssigned(candidate.Id);

            if (HasPositionAssigned)
            {
                errors.Add(CandidatesError.CandidateAssignedToPosition);
            }

            return errors.Any() ? ValidationResult.Failure(errors.ToArray()) : ValidationResult.Success();
        }

        public async Task<ValidationResult> ValidateCreateAsync(Candidates candidate)
        {
            var errors = new List<Error>();
            var hasElection = await _CandidateDomainService.IsElectionProcessActive();

            if (hasElection)
            {
                errors.Add(CandidatesError.ActiveElectionExists);
            }

            var PoliticalPartyIsActive = await _CandidateDomainService.IsPoliticalPartyActive(candidate.PoliticalPartyId);

            if (!PoliticalPartyIsActive)
            {
                errors.Add(CandidatesError.PoliticalPartyNotActive);
            }
            return errors.Any() ? ValidationResult.Failure(errors.ToArray()) : ValidationResult.Success();

        }

        public async Task<ValidationResult> ValidateUpdateAsync(Candidates candidate)
        {
            var errors = new List<Error>();
            var HasElectionActive = await _CandidateDomainService.IsElectionProcessActive();

            if (HasElectionActive == true)
            {
                errors.Add(CandidatesError.ActiveElectionExists);
            }


            if (candidate.HasParticipatedInElection)
            {
                errors.Add(CandidatesError.CandidateHasParticipatedElection);

            }

            return errors.Any() ? ValidationResult.Failure(errors.ToArray()) : ValidationResult.Success();
        }
    }
}
