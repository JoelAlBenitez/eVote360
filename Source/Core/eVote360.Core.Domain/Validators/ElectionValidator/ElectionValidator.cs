using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Settings.ValueObjects.ElectionDate;
using eVote360.Core.Domain.Contracts.ServiceValidates.Election;

namespace eVote360.Core.Domain.Validators.ElectionValidator
{
    public class ElectionValidator : IElectionValidator
    {
        private readonly IElectionDomainService _electionDomainService;

        public ElectionValidator(IElectionDomainService electionDomainService)
        {
            _electionDomainService = electionDomainService;
        }
        public async Task<ValidationResult> ValidateElection(Entities.Election.Election election)
        {
            var errors = new List<Error>();
            var validations = new[]
            {
                await ValidateElectionName (election.Name),
                await ValidateElectionDate(election.ElectionDate),
                await ExistActiveElection(election.Id)
            };

            errors.AddRange(validations.Where(v => v != null));
            return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success();
        }

        private async Task<Error> ExistActiveElection(int id)
        {
            var alreadyActive = await _electionDomainService.ExistActiveElection();
            if (alreadyActive) return ElectionError.ElectionStateError;
            return null!;
        }

        private async Task<Error> ValidateElectionDate(ElectionDate electionDate)
        {
            var validate = await _electionDomainService.ValidElectionDate(electionDate);
            if (!validate) return ElectionError.ElectionDateNotValid;
            return null!;
        }

        private async Task<Error> ValidateElectionName(string name)
        {
            var validate = await _electionDomainService.ExistElectionByName(name);
            if (validate) return ElectionError.ElectionNameNotValid;
            return null!;
        }
    }
}
