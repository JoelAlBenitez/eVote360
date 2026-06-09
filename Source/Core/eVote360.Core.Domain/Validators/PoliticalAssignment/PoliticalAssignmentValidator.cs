using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.DomainService.PoliticalAssignment;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace eVote360.Core.Domain.Validators.PoliticalAssignment
{
    public class PoliticalAssignmentValidator : IPoliticalAssignmentValidator
    {
        private readonly IPoliticalAssignmentDomainService _politicalAssignmentDomainService;

        public PoliticalAssignmentValidator(IPoliticalAssignmentDomainService politicalAssignmentDomainService)
        {
            _politicalAssignmentDomainService = politicalAssignmentDomainService;
        }

        public async Task<ValidationResult> ValidatePoliticalAssignment(Entities.PoliticalAssignment.PoliticalAssignment politicalAssignment)
        {
            var errors = new List<Error>();
            var validations = new[]
            {
               await ValidatePoliticalParty(politicalAssignment.PoliticalPartyId),
               await validatePoliticalLeader(politicalAssignment.PoliticalLeaderId),
            };
            errors.AddRange(validations.Where(v => v != null));
            return errors.Any() ? ValidationResult.Failure(errors.ToArray()) : ValidationResult.Success();
        }

        private async Task<Error> validatePoliticalLeader(int userId)
        {
            var hasLeaderRole = await _politicalAssignmentDomainService.UserHasLeaderRoleAsync(userId);
            if (!hasLeaderRole) return PoliticalAssignmentError.UserIsNotLeader;

            var UserIsActiveAsync = await _politicalAssignmentDomainService.UserIsActiveAsync(userId);
            if (!UserIsActiveAsync) return PoliticalAssignmentError.UserIsNotActive;

            var UserAlreadyAssignedAsync = await _politicalAssignmentDomainService.UserAlreadyAssignedAsync(userId);
            if (!UserAlreadyAssignedAsync) return PoliticalAssignmentError.HasAlreadyAnAssignment;

            return null!;
        }

        private async Task<Error> ValidatePoliticalParty(int partyId)
        {
            var isPartyActive = await _politicalAssignmentDomainService.IsPartyActiveAsync(partyId);
            if (!isPartyActive) return PoliticalAssignmentError.HasAlreadyALeader;

            var alreadyHasLeader = await _politicalAssignmentDomainService.PartyAlreadyHasLeaderAsync(partyId);
            if (alreadyHasLeader) return PoliticalAssignmentError.HasAlreadyALeader;

            return null!;
        }
    }
}
