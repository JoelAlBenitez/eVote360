
using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.ServiceValidates.Citizens;
using eVote360.Core.Domain.Contracts.ServiceValidates.Elector.CodeVerifications;

namespace eVote360.Core.Domain.Validators.ElectorValidator.CodeVerifications
{
    public class CodeVerificationsValidator : ICodeVerificationValidator
    {

        private readonly ICodeVerificationValidate _codeVerificationValidate;

        private readonly ICitizensServiceValidate _citizensServiceValidate;

        public CodeVerificationsValidator(ICodeVerificationValidate codeVerificationValidate,
                ICitizensServiceValidate citizensServiceValidate
            )
        {
            _codeVerificationValidate = codeVerificationValidate;
        }

        public async Task<ValidationResult> VerificationCode(Guid IdCitizens, int IdElection, int Code)
        {
            var _errors = new List<Error>();
            //agregar metodo en citizens para validar si existe o no y poder validar el codigo ingresado

            //agregar validacion de elecciones activas del services validator de la entidad 

            var citizenState = await _citizensServiceValidate.CurrentStateOfTheCitizen(IdCitizens);
            if (!citizenState) _errors.Add(new Error("Ciudadano no se encuentra activo para votar", "El estado del ciudadano no se encentra en vigencia de votos, favor intente de nuevo o notifique a las autoridades."));

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
