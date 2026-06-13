using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Contracts.ServiceValidates.PoliticalAlliance;
using eVote360.Core.Domain.Entities.PoliticalAlliances;
using System.Collections.Generic;
using System.Threading.Tasks;
using eVote360.Core.Domain.Common.Errors;

namespace eVote360.Core.Domain.Validators.PoliticalAlliancesValidator
{
    public class AllianceValidator : IAllianceValidator
    {
        private readonly IPoliticalAlliancesValidate _politicalAlliancesDomainService;

        public AllianceValidator(IPoliticalAlliancesValidate politicalAlliancesDomainService)
        {
            _politicalAlliancesDomainService = politicalAlliancesDomainService;
        }

        public async Task<ValidationResult> ValidateCreateRequestAsync(int requestingPartyId, int receivingPartyId)
        {
            var errors = new List<Error>();

            if (await _politicalAlliancesDomainService.IsElectionProcessActive())
            {
                errors.Add(AllianceErrors.CannotModifyDuringActiveElection);
                return ValidationResult.Failure(errors);
            }

            if (requestingPartyId == receivingPartyId)
            {
                errors.Add(AllianceErrors.CannotAllyWithOwnParty);
                return ValidationResult.Failure(errors);
            }

            if (!await _politicalAlliancesDomainService.IsPartyActive(receivingPartyId))
            {
                errors.Add(AllianceErrors.PartyNotActive);
                return ValidationResult.Failure(errors);
            }

            if (await _politicalAlliancesDomainService.HasActiveAlliance(requestingPartyId, receivingPartyId))
            {
                errors.Add(AllianceErrors.AllianceAlreadyExists);
                return ValidationResult.Failure(errors);
            }

            if (await _politicalAlliancesDomainService.HasPendingRequest(requestingPartyId, receivingPartyId))
            {
                errors.Add(AllianceErrors.PendingRequestAlreadyExists);
                return ValidationResult.Failure(errors);
            }

            return ValidationResult.Success();
        }

        public async Task<ValidationResult> ValidateAcceptRequestAsync(PoliticalAlliances alliance, int currentUserIdParty)
        {
            var errors = new List<Error>();

            if (alliance == null)
            {
                errors.Add(AllianceErrors.RequestNotFound);
                return ValidationResult.Failure(errors);
            }

            if (await _politicalAlliancesDomainService.IsElectionProcessActive())
            {
                errors.Add(AllianceErrors.CannotModifyDuringActiveElection);
                return ValidationResult.Failure(errors);
            }

            if (alliance.ReceivingPartyId != currentUserIdParty)
            {
                errors.Add(AllianceErrors.NoPermission);
                return ValidationResult.Failure(errors);
            }

            if (alliance.Status != AllianceStatus.Pending)
            {
                errors.Add(AllianceErrors.RequestAlreadyAnswered);
                return ValidationResult.Failure(errors);
            }

            if (await _politicalAlliancesDomainService.HasActiveAlliance(alliance.RequestingPartyId, alliance.ReceivingPartyId))
            {
                errors.Add(AllianceErrors.AllianceAlreadyExists);
                return ValidationResult.Failure(errors);
            }

            return ValidationResult.Success();
        }

        public async Task<ValidationResult> ValidateRejectRequestAsync(PoliticalAlliances alliance, int currentUserIdParty)
        {
            var errors = new List<Error>();

            if (alliance == null)
            {
                errors.Add(AllianceErrors.RequestNotFound);
                return ValidationResult.Failure(errors);
            }

            if (await _politicalAlliancesDomainService.IsElectionProcessActive())
            {
                errors.Add(AllianceErrors.CannotModifyDuringActiveElection);
                return ValidationResult.Failure(errors);
            }

            if (alliance.ReceivingPartyId != currentUserIdParty)
            {
                errors.Add(AllianceErrors.NoPermission);
                return ValidationResult.Failure(errors);
            }

            if (alliance.Status != AllianceStatus.Pending)
            {
                errors.Add(AllianceErrors.RequestAlreadyAnswered);
                return ValidationResult.Failure(errors);
            }

            return ValidationResult.Success();
        }

        public async Task<ValidationResult> ValidateDeleteRequestAsync(PoliticalAlliances alliance, int currentUserIdParty)
        {
            var errors = new List<Error>();

            if (alliance == null)
            {
                errors.Add(AllianceErrors.RequestNotFound);
                return ValidationResult.Failure(errors);
            }

            if (await _politicalAlliancesDomainService.IsElectionProcessActive())
            {
                errors.Add(AllianceErrors.CannotModifyDuringActiveElection);
                return ValidationResult.Failure(errors);
            }

            if (alliance.RequestingPartyId != currentUserIdParty)
            {
                errors.Add(AllianceErrors.NoPermission);
                return ValidationResult.Failure(errors);
            }

            if (alliance.Status == AllianceStatus.Accepted)
            {
                errors.Add(AllianceErrors.CannotDeleteAcceptedRequest);
                return ValidationResult.Failure(errors);
            }

            return ValidationResult.Success();
        }

        public async Task<ValidationResult> ValidateDeleteActiveAllianceAsync(PoliticalAlliances alliance, int currentUserIdParty)
        {
            var errors = new List<Error>();

            if (alliance == null)
            {
                errors.Add(AllianceErrors.AllianceNotFound);
                return ValidationResult.Failure(errors);
            }

            if (await _politicalAlliancesDomainService.IsElectionProcessActive())
            {
                errors.Add(AllianceErrors.CannotModifyDuringActiveElection);
                return ValidationResult.Failure(errors);
            }

            if (alliance.RequestingPartyId != currentUserIdParty && alliance.ReceivingPartyId != currentUserIdParty)
            {
                errors.Add(AllianceErrors.NoPermission);
                return ValidationResult.Failure(errors);
            }

            if (await _politicalAlliancesDomainService.HasAssignedCandidatesBetweenParties(alliance.RequestingPartyId, alliance.ReceivingPartyId))
            {
                errors.Add(AllianceErrors.CannotDeleteWithAssignedCandidates);
                return ValidationResult.Failure(errors);
            }

            return ValidationResult.Success();
        }
    }
}
