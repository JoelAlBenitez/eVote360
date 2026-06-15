using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Entities.Elector.SelectionElector;

namespace eVote360.Core.Domain.Validators.ElectorValidator.ProcessVotesElector
{
    public interface IProcessElector
    {
        Task<ValidationResult> ValidateProcessElectoral(IReadOnlyCollection<SelectionElector> selectionElectors);
    }
}
