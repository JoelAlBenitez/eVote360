using System;

namespace eVote360.Core.Domain.Entities.CandidateAssignment
{
    public class CandidateAssignment
    {
        public int Id { get; set; }
        public int AssigningPartyId { get; set; }
        public int CandidateId { get; set; }
        public int ElectivePositionId { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        public int CreateUserId { get; set; }

        // Propiedades de navegación - descomentar cuando todos los módulos estén en development
        // public Candidates Candidate { get; set; }
        // public ElectivePositions ElectivePosition { get; set; }
        // public PoliticalParty AssigningParty { get; set; }
    }
}
