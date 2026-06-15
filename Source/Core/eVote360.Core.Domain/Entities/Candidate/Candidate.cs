using eVote360.Core.Domain.Commom.BaseEntity;
using CandidateAssignmentEntity = eVote360.Core.Domain.Entities.CandidateAssignment.CandidateAssignment;
using PoliticalPartyEntity = eVote360.Core.Domain.Entities.PoliticalParty.PoliticalParty;
using eVote360.Core.Domain.Settings.ValueObjects.Candidate;
using System.Collections.Generic;

namespace eVote360.Core.Domain.Entities.Candidate
{
    public class Candidates : BaseEntitie<int, FullName>
    {
        public required CandidatePhoto PhotoUrl { get; set; }
        public int PoliticalPartyId { get; set; }
        public bool HasParticipatedInElection { get; set; }

        public PoliticalPartyEntity? Partido { get; set; }
        public ICollection<CandidateAssignmentEntity> AsignacionesPuestos { get; set; } = new List<CandidateAssignmentEntity>();
    }
}
