using eVote360.Core.Application.Contracts.Elector.Commands.Code;
using eVote360.Core.Application.DTOs.Elector.Code;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Validators.ElectorValidator.CodeVerifications;
using eVote360.Core.Application.Contracts.Elector.Commands.ElectorSession;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Contracts.Repositories.ElectionRepository;
using eVote360.Core.Domain.Contracts.Repositories.Citizens;

using eVote360.Core.Domain.Contracts.Repositories.Elector.Otp;

namespace eVote360.Core.Application.Services.Elector.CommandHandler.Code
{
    public class CodeVerificationCommandHandler : ICodeVerificationCommand
    {

        private readonly ICodeVerificationValidator _codeVerificationValidator;
        private readonly IElectorSession _electorSession;
        private readonly IElectionRepository _electionRepository;
        private readonly ICitizenRepository _citizenRepository;
        private readonly IOtpRepository _otpRepository;

        public CodeVerificationCommandHandler(ICodeVerificationValidator codeVerificationValidator,
            IElectorSession electorSession,
            IElectionRepository electionRepository,
            ICitizenRepository citizenRepository,
            IOtpRepository otpRepository
            )
        {
            _codeVerificationValidator = codeVerificationValidator;
            _electorSession = electorSession;
            _electionRepository = electionRepository;
            _citizenRepository = citizenRepository;
            _otpRepository = otpRepository;
        }

        public async Task<ValidationResult> VerifyCodeVerification(CodeVerificationDto codeVerificationDto)
        {
            var _errors = new List<Error>();
            try
            {
                var elecive = await _electionRepository.GetActivateElectionAsync();
                var citizen = await _citizenRepository.GetByIdentification(_electorSession.GetIdentification());
                var validate = await _codeVerificationValidator.VerificationCode(citizen.Id, elecive!.Id, codeVerificationDto.Code);
                if (!validate.IsValid) return validate;

                var otp = await _otpRepository.GetByIdAndIdCitizens(citizen.Id, elecive.Id);
                otp.State = true;
                await _otpRepository.UpdateAsync(otp);

                return ValidationResult.Success();
            }
            catch (Exception ex) {

                _errors.Add(new Error("Ha ocurrido un error inesperado", ex.Message));
                return ValidationResult.Failure(_errors);
            
            }
        }
    }
}
