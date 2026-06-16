using System;

namespace eVote360.Core.Application.ViewModels.CandidateAssignment
{
    public class CandidateAssignmentViewModel
    {
        public int? AssignmentId { get; set; }
        public int ElectivePositionId { get; set; }
        public string ElectivePositionName { get; set; } = string.Empty;
        public int? CandidateId { get; set; }
        public string? CandidateName { get; set; }
        public string? CandidateLastName { get; set; }
        public string? PhotoUrl { get; set; }
        public string? CandidateType { get; set; }
        public string? OriginPartyName { get; set; }
        public string? OriginPartySiglas { get; set; }
        public bool HasCandidate { get; set; }
    }
}
