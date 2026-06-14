namespace eVote360.Core.Domain.Entities.CandidateAssignment.ReadModels
{
    public class CandidateAssignmentReadModel
    {
        public int ElectivePositionId { get; set; }
        public string ElectivePositionName { get; set; } = string.Empty;
        
        public int? AssignmentId { get; set; }
        public int? CandidateId { get; set; }
        public string? CandidateName { get; set; }
        public string? CandidateLastName { get; set; }
        public string? PhotoUrl { get; set; }
        
        public string? CandidateType { get; set; } // "Propio" o "Aliado"
        public string? OriginPartyName { get; set; }
        public string? OriginPartySiglas { get; set; }
        
        public int AssigningPartyId { get; set; }
    }
}
