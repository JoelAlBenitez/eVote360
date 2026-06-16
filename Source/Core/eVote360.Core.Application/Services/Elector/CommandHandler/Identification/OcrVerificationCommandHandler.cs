using eVote360.Core.Application.Contracts.Elector.Commands.ElectorSession;
using eVote360.Core.Application.Contracts.Elector.Commands.Identification;
using eVote360.Core.Application.Contracts.EmailService;
using eVote360.Core.Application.DTOs.Elector.Identification;
using eVote360.Core.Application.DTOs.Message;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.Citizens;
using eVote360.Core.Domain.Contracts.Repositories.ElectionRepository;
using eVote360.Core.Domain.Contracts.Repositories.Elector.Otp;
using eVote360.Core.Domain.Contracts.ServiceValidates.Elector.OCR;
using eVote360.Core.Domain.Entities.Elector.CodeVerifications;
using eVote360.Core.Domain.Validators.ElectorValidator.IdentificationProcess;

namespace eVote360.Core.Application.Services.Elector.CommandHandler.Identification
{
    public class OcrVerificationCommandHandler : IOcrVerificationCommand
    {
        private readonly IOcrService _ocrService;
        private readonly IIdentificationProcess _identificationProcess;
        private readonly IElectorSession _electorSession;
        private readonly IOtpRepository _otpRepository;
        private readonly ICitizenRepository _citizenRepository;
        private readonly IElectionRepository _electionRepository;
        private readonly IEmailService _emailService;

        public OcrVerificationCommandHandler(
            IOcrService ocrService,
            IIdentificationProcess identificationProcess,
            IElectorSession electorSession,
            IOtpRepository otpRepository,
            ICitizenRepository citizenRepository,
            IElectionRepository electionRepository,
            IEmailService emailService)
        {
            _ocrService = ocrService;
            _identificationProcess = identificationProcess;
            _electorSession = electorSession;
            _otpRepository = otpRepository;
            _citizenRepository = citizenRepository;
            _electionRepository = electionRepository;
            _emailService = emailService;
        }

        public async Task<ValidationResult> VerifyOcrAndCreateCodeAsync(OcrVerificationDto ocrVerificationDto)
        {
            var errors = new List<Error>();
            try
            {
                string extractedText = _ocrService.ExtractTextByte(ocrVerificationDto.ImageBytes);
                if (string.IsNullOrEmpty(extractedText))
                {
                    errors.Add(new Error("Error OCR", "No se pudo extraer texto de la imagen proporcionada."));
                    return ValidationResult.Failure(errors);
                }

                string identificationEntered = _electorSession.GetIdentification();
                var comparisonResult = await _identificationProcess.ValidateComparadIdentificationByImg(extractedText, identificationEntered);
                
                if (!comparisonResult.IsValid) return comparisonResult;

                _electorSession.SetValidateOCR(true);

                var citizen = await _citizenRepository.GetByIdentification(identificationEntered);
                var election = await _electionRepository.GetActivateElectionAsync();

                if (election == null)
                {
                    errors.Add(new Error("Elección no encontrada", "No hay una elección activa en este momento."));
                    return ValidationResult.Failure(errors);
                }

                int code = new Random().Next(100000, 999999);

                var codeVerification = new CodeVerification
                {
                    IdCitizens = citizen.Id,
                    IdElection = election.Id,
                    Code = code,
                    State = false, 
                    CreateAt = DateTimeOffset.UtcNow,
                    ExpirationDate = DateTimeOffset.UtcNow.AddMinutes(5)
                };

                await _otpRepository.CreateAsync(codeVerification);

                await _emailService.SendEmailAsync(new MessageDto
                {
                    ToEmail = citizen.Email.Value,
                    Subject = "Código de Verificación eVote360",
                    Body = $"<h3>Código de Verificación</h3><p>Su código para votar es: <strong>{code}</strong></p><p>Este código expira en 5 minutos.</p>"
                });

                return ValidationResult.Success();
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Error inesperado", ex.Message));
                return ValidationResult.Failure(errors);
            }
        }
    }
}
