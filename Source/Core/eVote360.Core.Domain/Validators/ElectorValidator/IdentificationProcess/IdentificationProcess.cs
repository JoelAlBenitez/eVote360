using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.ServiceValidates.Citizens;
using eVote360.Core.Domain.Contracts.ServiceValidates.Elector.Votes;

namespace eVote360.Core.Domain.Validators.ElectorValidator.IdentificationProcess
{
    public class IdentificationProcess : IIdentificationProcess
    {
     
        private readonly ICitizensServiceValidate _citizensServiceValidate;
        private readonly IVotesValidate _votesValidate;
        public  List<Error> _errors = new List<Error>();

        public IdentificationProcess (ICitizensServiceValidate citizensServiceValidate,
            IVotesValidate votesValidate
            )
        {
            _citizensServiceValidate = citizensServiceValidate;
            _votesValidate = votesValidate;
        }

        public Task<ValidationResult> ValidateComparadIdentificationByImg(string IdentificationImg, string IdentificationEntered)
        {

            throw new NotImplementedException();
        }

        public Task<ValidationResult> ValidateEnteredIdentification(string Identification)
        {
            throw new NotImplementedException();
        }
    }
}
