

using eVote360.Core.Application.Contracts.Election.Commands;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.ElectionRepository;
using eVote360.Core.Domain.Validators.ElectionValidator;

namespace eVote360.Core.Application.Services.Election.CommandHandler
{
    public sealed class ElectionAlterState : IElectionAlterStateCommand
    {
        private readonly IElectionRepository _repository;
        public ElectionAlterState(IElectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult> ExecuteAsync(int id)
        {
            var result = await _repository.DeactivateElectionAsync(id);

            if (!result) {
                var errors = new List<Error>
                {
                    new Error ("ELEC ERROR","No se encontro la eleccion")
                };
                return ValidationResult.Failure(errors);
            }
            return ValidationResult.Success();
        }
    }
}
