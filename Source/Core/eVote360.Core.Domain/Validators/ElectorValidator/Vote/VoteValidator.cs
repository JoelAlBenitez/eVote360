using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.DomainService.ElectivePosition;
using eVote360.Core.Domain.Contracts.ServiceValidates.Candidate;
using eVote360.Core.Domain.Contracts.ServiceValidates.Citizens;
using eVote360.Core.Domain.Contracts.ServiceValidates.Elector.Votes;
using eVote360.Core.Domain.Entities.Elector.AuditVote;
using eVote360.Core.Domain.Entities.Elector.Vote;

namespace eVote360.Core.Domain.Validators.ElectorValidator.Vote
{
    public class VoteValidator : IVoteValidator
    {

        private readonly IVotesValidate _votesValidate;
        private readonly ICitizensServiceValidate _citizensServiceValidate;
        private readonly ICandidateDomainService _candidateDomainService;
        private readonly IElectivePositionValidate _electivePositionValidate;

        //agregar el validate de elecciones que permita determinar si hay una eleccion activa o no

        public VoteValidator(IVotesValidate votesValidate, 
            ICitizensServiceValidate citizensServiceValidate,
            ICandidateDomainService candidateDomainService,
            IElectivePositionValidate electivePositionValidate
            )
        {
            _votesValidate = votesValidate;
            _citizensServiceValidate = citizensServiceValidate;
            _candidateDomainService = candidateDomainService;
            _electivePositionValidate = electivePositionValidate;
        }


        public Task<ValidationResult> ValidateCreate(Votes vote, AuditVotes auditVotes)
        {
            throw new NotImplementedException(); //agregar implementancion cuando se procesen con cambios en los services pertinentes hacer utilizados
        }
    }
}
