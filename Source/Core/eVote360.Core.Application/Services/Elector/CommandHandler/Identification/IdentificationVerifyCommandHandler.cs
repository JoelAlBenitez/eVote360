using eVote360.Core.Application.Contracts.Elector.Commands.Identification;
using eVote360.Core.Application.DTOs.Elector.Identification;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Validators.ElectorValidator.IdentificationProcess;

namespace eVote360.Core.Application.Services.Elector.CommandHandler.Identification
{
    public class IdentificationVerifyCommandHandler : IIdentificationVerifyCommand
    {

        private readonly IIdentificationProcess _identificationProcess;
        public IdentificationVerifyCommandHandler(IIdentificationProcess identificationProcess)
        {
            _identificationProcess = identificationProcess;
        }

        public async Task<ValidationResult> VerifyIdentificationByCitizen(IdentificiationDto identificiationDto)
        {
            var errors = new List<Error>();
            try
            {
                var validate = await _identificationProcess.ValidateEnteredIdentification(identificiationDto.identification);
                if (!validate.IsValid) return validate;
                return ValidationResult.Success();
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Ha ocurrido un error inesperado", ex.Message));
                return ValidationResult.Failure(errors);
            }
        }
    }
}
