using eVote360.Core.Application.Contracts.Elector.Commands.ElectorSession;
using eVote360.Core.Application.Contracts.Elector.Commands.Identification;
using eVote360.Core.Application.DTOs.Elector.Identification;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Validators.ElectorValidator.IdentificationProcess;

namespace eVote360.Core.Application.Services.Elector.CommandHandler.Identification
{
    public class IdentificationVerifyCompareIdentificationByImg : IIdentificationVerifyCompareIdentificationByImg
    {

        private readonly IIdentificationProcess _identificationProcess;
        private readonly IElectorSession _electorSession;

        public IdentificationVerifyCompareIdentificationByImg(
            IIdentificationProcess identificationProcess,
            IElectorSession electorSession
            )
        {
            _identificationProcess = identificationProcess;
            _electorSession = electorSession;
        }

        public async Task<ValidationResult> VerifyIdentificationComparedByImg(IdentificiationDto identificiationDto)
        {
            var _errors = new List<Error>();
            try
            {
                var validate = await _identificationProcess.
                    ValidateComparadIdentificationByImg(identificiationDto.identification, 
                    _electorSession.GetIdentification());
                if (validate != null) return validate;
                return ValidationResult.Success();
            }
            catch (Exception ex)
            {
                _errors.Add(new Error("Ha ocurrido un error inesperado", ex.Message));
                return ValidationResult.Failure(_errors);

            }
        }
    }
}
