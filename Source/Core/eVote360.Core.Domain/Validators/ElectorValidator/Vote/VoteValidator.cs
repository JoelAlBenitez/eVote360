using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.DomainService.ElectivePosition;
using eVote360.Core.Domain.Contracts.ServiceValidates.Candidate;
using eVote360.Core.Domain.Contracts.ServiceValidates.Citizens;
using eVote360.Core.Domain.Contracts.ServiceValidates.Election;
using eVote360.Core.Domain.Contracts.ServiceValidates.Elector.Votes;
using eVote360.Core.Domain.Entities.Elector.AuditVote;
using eVote360.Core.Domain.Entities.Elector.Vote;
using eVote360.Core.Domain.Common.CodeErrors;

namespace eVote360.Core.Domain.Validators.ElectorValidator.Vote
{
    public class VoteValidator : IVoteValidator
    {

        private readonly IVotesValidate _votesValidate;
        private readonly ICitizensServiceValidate _citizensServiceValidate;
        private readonly ICandidateDomainService _candidateDomainService;
        private readonly IElectivePositionValidate _electivePositionValidate;
        private readonly IElectionDomainService _electionDomainService;
        public VoteValidator(IVotesValidate votesValidate, 
            ICitizensServiceValidate citizensServiceValidate,
            ICandidateDomainService candidateDomainService,
            IElectivePositionValidate electivePositionValidate,
            IElectionDomainService electionDomainService
            )
        {
            _votesValidate = votesValidate;
            _citizensServiceValidate = citizensServiceValidate;
            _candidateDomainService = candidateDomainService;
            _electivePositionValidate = electivePositionValidate;
            _electionDomainService = electionDomainService;
        }

        public async Task<ValidationResult> ValidateCreate(Votes vote, AuditVotes auditVotes)
        {
            var errrors = new List<Error>();
            var electionActive = await _electionDomainService.ExistActiveElection();
            if (vote == null || auditVotes == null) {
                errrors.Add(VotesError.DataInvalid);
                ValidationResult.Failure(errrors);
             }
            if (!electionActive)
            {
                errrors.Add(VotesError.ElectoralProcessNoValid);
                return ValidationResult.Failure(errrors);
            }
            var citizenActive = await _citizensServiceValidate.ExistByIdCitizen(auditVotes!.IdCitizen);
            if (!citizenActive)
            {
                errrors.Add(CitizenErrors.NoExistCitzentById);
                return ValidationResult.Failure(errrors);
            }
            var candidacteActive = await _candidateDomainService.CandidateExistsAsync(vote!.IdCandidate);
            if (!citizenActive)
            {
                errrors.Add(new Error("Candidacto no encontrado", "El candidacto seleccionado en el proceso electoral no fue encontrado, " +
                    "si se trata de un errror favor refrescar la pagina o en su defecto contactar con un administrador."));
                return ValidationResult.Failure(errrors);
            }
            var posictionElective = await _electivePositionValidate.ExistById(vote.IdElectivePosiction);
            if (!posictionElective)
            {
                errrors.Add(ElectivePosictionsError.NonExistentElectivePosition);
                return ValidationResult.Failure(errrors);
            }
            return ValidationResult.Success();
        }

        public async Task<ValidationResult> ValidateStates(Votes vote, AuditVotes auditVotes)
        {
            var errrors = new List<Error>();

            var citizenState = await _citizensServiceValidate.CurrentStateCitizen(auditVotes.IdCitizen);
            if (!citizenState) {

                errrors.Add(CitizenErrors.CitizentNoActiveOfVote);
                return ValidationResult.Failure(errrors);
            }
            var candidacteState = await _candidateDomainService.GetCandidateStateAsync(vote.IdCandidate);
            if (!citizenState) { 
                
                errrors.Add(new Error("Candidacto no valido", "El candidacto seleccionado no se encuentra en disponibilidad, favor contactar con el equipo de administración."));
                return ValidationResult.Failure(errrors);
            }
            var positionState = await _electivePositionValidate.CurrentStateElectivePosiction(vote.IdElectivePosiction);
            if (!positionState) {
                errrors.Add(new Error("Puesto electivo no valido", "El puesto electivo seleccionado presenta problemas favor refrescar la pagina o intente de nuevo más tarde. "));
                return ValidationResult.Failure(errrors);
            }

            return errrors.Any() ? ValidationResult.Failure(errrors) : ValidationResult.Success();
        }
    }
}
