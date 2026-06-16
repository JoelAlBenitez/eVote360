using eVote360.Core.Application.Contracts.Elector.Commands.ElectorSession;
using eVote360.Core.Application.Contracts.Elector.Commands.Votes;
using eVote360.Core.Application.DTOs.Elector.Select;
using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.Citizens;
using eVote360.Core.Domain.Contracts.Repositories.ElectionRepository;
using eVote360.Core.Domain.Entities.Elector.AuditVote;
using eVote360.Core.Domain.Entities.Elector.SelectionElector;
using eVote360.Core.Domain.Validators.ElectorValidator.ProcessVotesElector;
using IVotingProcessContract = eVote360.Core.Domain.Contracts.Repositories.Elector.Vote.IVotingProcess;
using IVoteValidatorContract = eVote360.Core.Domain.Validators.ElectorValidator.Vote.IVoteValidator;
using VotesEntity = eVote360.Core.Domain.Entities.Elector.Vote.Votes;

using eVote360.Core.Application.Contracts.EmailService;
using eVote360.Core.Application.DTOs.Message;

namespace eVote360.Core.Application.Services.Elector.CommandHandler.Votes
{
    public class ProcessVoteElectorCommandHandler : IProcessVoteElectorCommand
    {
        private readonly IElectorSession _electorSession;
        private readonly IProcessElector _processElector;
        private readonly IVoteValidatorContract _voteValidator;
        private readonly IVotingProcessContract _votingProcess;
        private readonly ICitizenRepository _citizenRepository;
        private readonly IElectionRepository _electionRepository;
        private readonly IEmailService _emailService;

        public ProcessVoteElectorCommandHandler(
            IElectorSession electorSession,
            IProcessElector processElector,
            IVoteValidatorContract voteValidator,
            IVotingProcessContract votingProcess,
            ICitizenRepository citizenRepository,
            IElectionRepository electionRepository,
            IEmailService emailService)
        {
            _electorSession = electorSession;
            _processElector = processElector;
            _voteValidator = voteValidator;
            _votingProcess = votingProcess;
            _citizenRepository = citizenRepository;
            _electionRepository = electionRepository;
            _emailService = emailService;
        }

        public async Task<ValidationResult> ProcessVoteAsync(List<SelectedVoteDto> selectedVotes)
        {
            var errors = new List<Error>();
            try
            {
                var identification = _electorSession.GetIdentification();
                var citizen = await _citizenRepository.GetByIdentification(identification);
                var election = await _electionRepository.GetActivateElectionAsync();

             
                var selectionElectors = selectedVotes.Select(v => new SelectionElector
                {
                    IdPosictionElective = v.IdElectivePosition,
                    IdCandidacte = v.IdCandidate ?? 0,
                    NotSelectionCandidate = v.IsNoApply
                }).ToList();

                var validateProcess = await _processElector.ValidateProcessElectoral(
                    citizen, election!, selectionElectors, ValidateOCR: _electorSession.GetValidateOCR());

                if (!validateProcess.IsValid) return validateProcess;

                var votes = selectedVotes.Select(v => new VotesEntity
                {
                    IdElection = election!.Id,
                    IdElectivePosiction = v.IdElectivePosition,
                    IdCandidate = v.IdCandidate ?? 0
                }).ToList();

                var auditVotes = new AuditVotes
                {
                    Id = Guid.NewGuid(),
                    IdCitizen = citizen.Id,
                    IdElection = election!.Id,
                    CreatAt = DateTimeOffset.UtcNow
                };

                var validateCreate = await _voteValidator.ValidateCreate(votes, auditVotes);
                if (!validateCreate.IsValid) return validateCreate;

                var validateStates = await _voteValidator.ValidateStates(votes, auditVotes);
                if (!validateStates.IsValid) return validateStates;

                var saved = await _votingProcess.CreateAsync(votes, auditVotes);
                if (!saved)
                {
                    errors.Add(new Error("Error al guardar", "No fue posible registrar su voto, intente nuevamente."));
                    return ValidationResult.Failure(errors);
                }

                var body = $"<h3>Resumen de votación - eVote360</h3>" +
                           $"<p>Hola {citizen.Name} {citizen.LastName},</p>" +
                           $"<p>Su voto ha sido procesado exitosamente para la elección: <strong>{election!.Name}</strong>.</p>" +
                           $"<p>Gracias por ejercer su derecho al voto.</p>";

                await _emailService.SendEmailAsync(new MessageDto
                {
                    ToEmail = citizen.Email.Value,
                    Subject = "Resumen de su Votación - eVote360",
                    Body = body
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
