using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Entities.ElectivePosition;

namespace eVote360.Core.Domain.Validators.ElectivePositionValidator
{
    public interface IElectivePositionsValidator
    {
            Task<ValidationResult> ValidateCreateElectivePositions(ElectivePositions electivePositions);
            Task<ValidationResult> ValidateUpdateElectivePosition(ElectivePositions electivePositions);
            Task<ValidationResult> ValidateActiveElectivePostions(int Id, string name);
            Task<ValidationResult> ValidateDesactiveElectivePositions(int Id, string name);
            
    }
}
