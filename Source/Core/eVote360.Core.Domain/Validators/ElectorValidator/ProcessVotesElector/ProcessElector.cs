using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.DomainService.ElectivePosition;
using eVote360.Core.Domain.Contracts.ServiceValidates.Candidate;
using eVote360.Core.Domain.Contracts.ServiceValidates.Citizens;
using eVote360.Core.Domain.Contracts.ServiceValidates.Election;
using eVote360.Core.Domain.Entities.Citizens;
using eVote360.Core.Domain.Entities.Election;
using eVote360.Core.Domain.Entities.Elector.SelectionElector;
using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Contracts.ServiceValidates.Elector.Votes;

namespace eVote360.Core.Domain.Validators.ElectorValidator.ProcessVotesElector
{
    public class ProcessElector : IProcessElector
    {

        private readonly IElectionDomainService _electionDomainService;
        private readonly IElectivePositionValidate _electivePositionValidate;
        private readonly ICandidateDomainService _candidateDomainService;
        private readonly ICitizensServiceValidate _citizensServiceValidate;
        private readonly IVotesValidate _votesValidate;

        public ProcessElector(IElectionDomainService electionDomainService,
            IElectivePositionValidate electivePositionValidate, 
            ICandidateDomainService candidateDomainService,
            ICitizensServiceValidate citizensServiceValidate,
            IVotesValidate votesValidate   
            )
        {
            _electionDomainService = electionDomainService;
            _electivePositionValidate = electivePositionValidate;
            _candidateDomainService = candidateDomainService;
            _citizensServiceValidate = citizensServiceValidate;
            _votesValidate = votesValidate;
        }

        public async Task<ValidationResult> ValidateProcessElectoral(
            Citizen citizen,
            Election election,
            List<SelectionElector> selectionElectors)
        {
            var errors = new List<Error>();

            var existElection = await _electionDomainService.ExistActiveElection();
            if (!existElection || election == null)
            {
                errors.Add(VotesError.ElectoralProcessNoValid);
                return ValidationResult.Failure(errors);
            }
            var citizenExits = await _citizensServiceValidate.CurrentStateCitizen(citizen.Id);
            if (!citizenExits)
            {
                errors.Add(CitizenErrors.NoExtisCitizen);
                return ValidationResult.Failure(errors);
            }

            var votesRealizedByCitizen = await _votesValidate.ExistVoteByCitizen(citizen.IdentificationNumber.Value);
            if (!votesRealizedByCitizen)
            {
                errors.Add(VotesError.ExistVotes);
                return ValidationResult.Failure(errors);
            }
            
            //

            //var positionsrequerid = election.ElectivePositions.Select(p => p.Id).ToList();

            var positionsSelectionsByCitizen = selectionElectors.Select(s => s.IdPosictionElective).ToList();

            // var positionsInvalids = positionsrequerid.Except(positionsSelectionsByCitizen).ToList();

            /* if(positionsInvalids.Any())
            {
                 var missingnames = election.ElectionPosition
                    .Where(p => p.positionsInvalid.Contains(p.Id))
                    .Select(p => p.Nombre)
                    .ToList();

                string positionsC = string.Join(", ", missingnames) + ".";
                
                errors.Add(new Error("Uh al parecer ha dejado puestos electivos sin seleccionar", positionsC));
                
                return ValidationResult.Failure(errors);
            }

             */
            return ValidationResult.Success();
        }
    }
}
