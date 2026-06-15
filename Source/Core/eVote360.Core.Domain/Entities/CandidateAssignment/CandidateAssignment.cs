using System;
using eVote360.Core.Domain.Entities.Candidate;
using eVote360.Core.Domain.Entities.ElectivePosition;

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

     
      

        // Propiedades de navegación
        public Candidates Candidate { get; set; }
        public ElectivePositions ElectivePosition { get; set; }
    }
}
