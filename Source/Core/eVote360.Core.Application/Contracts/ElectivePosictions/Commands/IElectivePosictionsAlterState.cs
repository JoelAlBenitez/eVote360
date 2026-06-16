using eVote360.Core.Application.DTOs.ElectivePositions;
using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Contracts.ElectivePosictions.Commands
{
    public interface IElectivePosictionsAlterState
    {
        Task<ValidationResult> AlterState(ElectivePosictionsDesactiveOrActive dto);
    }
}
