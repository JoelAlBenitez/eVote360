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

        public async Task<ValidationResult> ValidateComparadIdentificationByImg(string IdentificationImg,
            string IdentificationEntered)
        {

            var errors = new List<Error>();
            if(IdentificationImg == null && IdentificationEntered == null)
            {
                errors.Add(new Error("Identificación no valida", "La indentificación ingresada y procesada no es valida, favor intentelo de nuevo"));
                return ValidationResult.Failure(errors);
            }

            var citizenExist = await _citizensServiceValidate.ExistCitizensByIdentification(IdentificationImg!);
            if (!citizenExist) errors.Add(CitizenErrors.NoExtisCitizen);
            if (IdentificationImg != IdentificationEntered) errors.Add(new Error("La identificación de la imagen no es valida", "La identificación de la imagen no coincide con la ingresada anteriormente."));
        
            return  errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success();
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

            var citizenExits = await _citizensServiceValidate.ExistCitizensByIdentification(Identification);
            if (!citizenExits) errors.Add(CitizenErrors.NoExtisCitizen);
            
            if (citizenExits)
            {
                var citizenState = await _citizensServiceValidate.CurrentStateOfTheCitizen(null, Identification);
                if (!citizenState) errors.Add(CitizenErrors.CitizentNoActiveOfVote);

                var citizenParticiped = await _votesValidate.ExistVoteByCitizen(Identification);
                if (!citizenParticiped) errors.Add(VotesError.ExistVotes);
            }
            
            return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success();
        }
    }
}
