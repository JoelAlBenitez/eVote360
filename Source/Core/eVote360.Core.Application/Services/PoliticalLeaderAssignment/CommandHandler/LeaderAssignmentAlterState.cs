

using eVote360.Core.Application.Contracts.PoliticalLeaderAssignment.Commands;
using eVote360.Core.Application.DTOs.PoliticalLeaderAssignment;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalAssignment;
using eVote360.Core.Domain.Validators.PoliticalAssignment;

namespace eVote360.Core.Application.Services.PoliticalLeaderAssignment.CommandHandler
{
    public sealed class LeaderAssignmentAlterState : ILeaderAssignmentAlterStateCommand
    {
        private readonly IPoliticalAssignmentRepository _repository;

        public LeaderAssignmentAlterState(IPoliticalAssignmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult> ExecuteAsync(int id, bool state )
        {
            var result = await _repository.AlterState(id, state);
                
                if (!result)
                {
                    var errors = new List<Error> { new Error("ASSIG ALTER STATE", "No se pudo cambiar el estado de la asignacion") };
                    return ValidationResult.Failure(errors);
                }
            

            return ValidationResult.Success();
        }
    }
}
