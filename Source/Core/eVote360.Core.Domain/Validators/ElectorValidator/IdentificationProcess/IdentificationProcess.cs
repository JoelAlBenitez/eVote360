using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.ServiceValidates.Citizens;
using eVote360.Core.Domain.Contracts.ServiceValidates.Election;
using eVote360.Core.Domain.Contracts.ServiceValidates.Elector.Votes;
using eVote360.Core.Domain.Common.CodeErrors;

namespace eVote360.Core.Domain.Validators.ElectorValidator.IdentificationProcess
{
    public class IdentificationProcess : IIdentificationProcess
    {
     
        private readonly ICitizensServiceValidate _citizensServiceValidate;
        private readonly IVotesValidate _votesValidate;
        private readonly IElectionDomainService _electionDomainService;
        public  List<Error> _errors = new List<Error>();

        public IdentificationProcess (ICitizensServiceValidate citizensServiceValidate,
            IVotesValidate votesValidate,
            IElectionDomainService electionDomainService
            )
        {
            _citizensServiceValidate = citizensServiceValidate;
            _votesValidate = votesValidate;
            _electionDomainService = electionDomainService;
        }

        public Task<ValidationResult> ValidateComparadIdentificationByImg(string IdentificationImg,
            string IdentificationEntered)
        {

            throw new NotImplementedException();
        }

        public async Task<ValidationResult> ValidateEnteredIdentification(string Identification)
        {
            var errors = new List<Error>();
            var electivActive = await _electionDomainService.ExistActiveElection();
            if(!electivActive)
            {
                errors.Add(VotesError.ElectoralProcessNoValid);
                return ValidationResult.Failure(errors);
            }

            //var citizenState = await _citizensServiceValidate.CurrentStateCitizen();
            //if (!citizenState) _errors.Add(CitizenErrors.CitizentNoActiveOfVote);
            

            return ValidationResult.Success();
        }
    }
}
