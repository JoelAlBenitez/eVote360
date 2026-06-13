using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalLeaderAssignment;
using eVote360.Core.Domain.Contracts.ServiceValidates.PoliticalLeaderAssignment;
using eVote360.Core.Domain.Contracts.Validators.PoliticalLeaderAssignment;
using System.Threading.Tasks;
using System.Linq;

namespace eVote360.Core.Domain.Validators.LeaderAssignmentValidator
{
    public class LeaderAssignmentValidator : ILeaderAssignmentValidator
    {
        private readonly IPoliticalLeaderAssignmentDomainService _domainService;
        private readonly IPoliticalLeaderAssignmentRepository _repository;

        public LeaderAssignmentValidator(
            IPoliticalLeaderAssignmentDomainService domainService,
            IPoliticalLeaderAssignmentRepository repository)
        {
            _domainService = domainService;
            _repository = repository;
        }

        public async Task<ValidationResult> ValidateCreateAsync(int userId, int partyId)
        {
            // 1. ¿Elección activa?
            if (await _domainService.IsElectionProcessActive())
                return ValidationResult.Failure(LeaderAssignmentErrors.ActiveElectionExists);

            // 2. ¿Usuario existe?
            if (!await _domainService.UserExists(userId))
                return ValidationResult.Failure(LeaderAssignmentErrors.UserNotFound);

            // 3. ¿Usuario activo?
            if (!await _domainService.UserIsActive(userId))
                return ValidationResult.Failure(LeaderAssignmentErrors.UserNotActive);

            // 4. ¿Usuario tiene rol DirigentePolitico?
            if (!await _domainService.UserHasLeaderRole(userId))
                return ValidationResult.Failure(LeaderAssignmentErrors.UserNotLeaderRole);

            // 5. ¿Usuario ya tiene partido asignado?
            if (await _repository.ExistsAssignmentForUser(userId))
                return ValidationResult.Failure(LeaderAssignmentErrors.UserAlreadyAssigned);

            // 6. ¿Partido existe?
            if (!await _domainService.PartyExists(partyId))
                return ValidationResult.Failure(LeaderAssignmentErrors.PartyNotFound);

            // 7. ¿Partido activo?
            if (!await _domainService.PartyIsActive(partyId))
                return ValidationResult.Failure(LeaderAssignmentErrors.PartyNotActive);

            // 8. ¿Partido ya tiene dirigente?
            if (await _repository.ExistsAssignmentForParty(partyId))
                return ValidationResult.Failure(LeaderAssignmentErrors.PartyAlreadyHasLeader);

            // 9. Todo correcto
            return ValidationResult.Success();
        }

        public async Task<ValidationResult> ValidateDeleteAsync(int assignmentId)
        {
            // 1. ¿Elección activa?
            if (await _domainService.IsElectionProcessActive())
                return ValidationResult.Failure(LeaderAssignmentErrors.ActiveElectionDeleteForbidden);

            // 2. ¿Asignación existe?
            var assignment = await _repository.GetByIdEntitie(assignmentId);
            if (assignment == null)
                return ValidationResult.Failure(LeaderAssignmentErrors.AssignmentNotFound);

            // 3. Todo correcto
            return ValidationResult.Success();
        }
    }
}
