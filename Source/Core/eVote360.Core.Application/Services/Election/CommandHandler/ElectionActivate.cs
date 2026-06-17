using eVote360.Core.Application.Contracts.Election.Commands;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.ElectionRepository;

namespace eVote360.Core.Application.Services.Election.CommandHandler
{
    public sealed class ElectionActivate : IElectionActivateCommand
    {
        private readonly IElectionRepository _repository;

        public ElectionActivate(IElectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult> ExecuteAsync(int id)
        {
            var result = await _repository.ActivateElectionAsync(id);

            if (!result)
            {
                var errors = new List<Error>
                {
                    new Error("ELEC ACTIVATE", "No se pudo activar la elección. Verifique que exista y esté en estado Pendiente.")
                };
                return ValidationResult.Failure(errors);
            }

            return ValidationResult.Success();
        }
    }
}
