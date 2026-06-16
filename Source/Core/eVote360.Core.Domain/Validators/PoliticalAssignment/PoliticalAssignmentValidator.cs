using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.ServiceValidates.PoliticalAssignment;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using eVote360.Core.Domain.Contracts.ServiceValidates.Election;


namespace eVote360.Core.Domain.Validators.PoliticalAssignment
{
    public class PoliticalAssignmentValidator : IPoliticalAssignmentValidator
    {
        private readonly IPoliticalAssignmentDomainService _politicalAssignmentDomainService;
        private readonly IElectionDomainService _electionDomainService;

        public PoliticalAssignmentValidator(IPoliticalAssignmentDomainService politicalAssignmentDomainService, IElectionDomainService electionDomainService)
        {
            _politicalAssignmentDomainService = politicalAssignmentDomainService;
            _electionDomainService = electionDomainService;
        }

        public async Task<ValidationResult> ValidatePoliticalAssignment(Entities.PoliticalAssignment.PoliticalAssignment politicalAssignment)
        {
            var errors = new List<Error>();
            var validations = new[]
            {
               await ValidatePoliticalParty(politicalAssignment.PoliticalPartyId),
               await validatePoliticalLeader(politicalAssignment.PoliticalLeaderId),
               await ValidateElectionState()
            };
            errors.AddRange(validations.Where(v => v != null));
            return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success();
        }

        private async Task<Error> validatePoliticalLeader(int userId)
        {
            var hasLeaderRole = await _politicalAssignmentDomainService.UserHasLeaderRoleAsync(userId);
            if (!hasLeaderRole) return PoliticalAssignmentError.UserIsNotLeader;

            var UserIsActiveAsync = await _politicalAssignmentDomainService.UserIsActiveAsync(userId);
            if (!UserIsActiveAsync) return PoliticalAssignmentError.UserIsNotActive;

            var UserAlreadyAssignedAsync = await _politicalAssignmentDomainService.UserAlreadyAssignedAsync(userId);
            if (UserAlreadyAssignedAsync) return PoliticalAssignmentError.HasAlreadyAnAssignment;

            return null!;
        }

        private async Task<Error> ValidatePoliticalParty(int partyId)
        {
            var isPartyActive = await _politicalAssignmentDomainService.IsPartyActiveAsync(partyId);
            if (!isPartyActive) return PoliticalAssignmentError.PartyIsNotActivated;

            var alreadyHasLeader = await _politicalAssignmentDomainService.PartyAlreadyHasLeaderAsync(partyId);
            if (alreadyHasLeader) return PoliticalAssignmentError.HasAlreadyALeader;

            return null!;
        }

        private async Task<Error> ValidateElectionState()
        {
           var isElectionActive = await _electionDomainService.ExistActiveElection();
           if (isElectionActive)
               return new Error("ASSIGN_LOCKED", "No se pueden realizar asignaciones mientras exista una elección activa en el sistema.");
            return null!;
        }
        
}
}
