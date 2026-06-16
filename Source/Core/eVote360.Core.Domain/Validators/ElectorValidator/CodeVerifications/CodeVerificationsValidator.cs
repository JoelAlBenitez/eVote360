using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.ServiceValidates.Citizens;
using eVote360.Core.Domain.Contracts.ServiceValidates.Election;
using eVote360.Core.Domain.Contracts.ServiceValidates.Elector.CodeVerifications;

namespace eVote360.Core.Domain.Validators.ElectorValidator.CodeVerifications
{
    public class CodeVerificationsValidator : ICodeVerificationValidator
    {

        private readonly ICodeVerificationValidate _codeVerificationValidate;
        private readonly ICitizensServiceValidate _citizensServiceValidate;
        private readonly IElectionDomainService _electionDomainService;

        public CodeVerificationsValidator(ICodeVerificationValidate codeVerificationValidate,
                ICitizensServiceValidate citizensServiceValidate,
                IElectionDomainService electionDomainService
            )
        {
            _codeVerificationValidate = codeVerificationValidate;
            _citizensServiceValidate = citizensServiceValidate;
            _electionDomainService = electionDomainService;
        }

        public async Task<ValidationResult> VerificationCode(Guid IdCitizens, int IdElection, int Code)
        {
            var _errors = new List<Error>();

            var electionActive = await _electionDomainService.ExistActiveElection();
            if (!electionActive)
            {
                _errors.Add(ElectionError.ElectionActive);
                return ValidationResult.Failure(_errors);
            }

            var citizenExist = await _citizensServiceValidate.ExistByIdCitizen(IdCitizens);
            if (!citizenExist) {
                _errors.Add(CitizenErrors.NoExistCitzentById);
                return ValidationResult.Failure(_errors);
            }

            var citizenState = await _citizensServiceValidate.CurrentStateOfTheCitizen(IdCitizens);
            if (!citizenState) _errors.Add(CitizenErrors.CitizentNoActiveOfVote);

            if (Code >= 100000 && Code <= 999999) _errors.Add(CodeVerificationError.CodeInvalid);

            var existActive = await _codeVerificationValidate.ExistCodeVerificationActive(IdCitizens, IdElection);
            if (existActive) _errors.Add(CodeVerificationError.CodeVerificationActive);

            var codeExpire = await _codeVerificationValidate.CodeExpire(IdCitizens, IdElection);
            if (codeExpire) _errors.Add(CodeVerificationError.CodeAlreadyUsed);

            var codeUse = await _codeVerificationValidate.CodeUse(IdCitizens, IdElection);
            if (codeUse) _errors.Add(CodeVerificationError.InvalidCodeVerification);

            var existCode = await _codeVerificationValidate.ExistCodeVerification(IdCitizens, IdElection);
            if (!existCode)
            {
                _errors.Add(CodeVerificationError.NoExistCodeVerification);
                _errors.Add(CodeVerificationError.CodeVerificationFailed);
            }

            var codeMatches = await _codeVerificationValidate.CodeMatchesWithRecord(Code, IdCitizens, IdElection);
            if (!codeMatches) _errors.Add(CodeVerificationError.CodeVerificationFailed);

            return _errors.Any() ? ValidationResult.Failure() : ValidationResult.Success();
        }
    }
}
