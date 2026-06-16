using eVote360.Core.Domain.Common.ValidationResult;


namespace eVote360.Core.Domain.Validators.PoliticalAssignment
{
    public interface IPoliticalAssignmentValidator
    {
        Task<ValidationResult> ValidatePoliticalAssignment(Entities.PoliticalAssignment.PoliticalAssignment politicalAssignment);
    }
}
