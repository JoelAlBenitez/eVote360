namespace eVote360.Core.Application.ViewModels.CandidateAssignment
{
    public class DeleteAssignmentViewModel
    {
        public int AssignmentId { get; set; }
        public string CandidateName { get; set; } = string.Empty;
        public string ElectivePositionName { get; set; } = string.Empty;
    }
}
