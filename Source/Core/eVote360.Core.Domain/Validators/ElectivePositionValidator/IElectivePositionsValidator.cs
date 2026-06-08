using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Domain.Validators.ElectivePositionValidator
{
    public interface IElectivePositionsValidator
    {
        Task<ValidationResult> ValidateElectivePositios(Entities.ElectivePosition.ElectivePositions elective);
    }
}
