using eVote360.Core.Domain.Commom.BaseEntity;
using eVote360.Core.Domain.Entities.Candidate.ValueObjects;

namespace eVote360.Core.Domain.Entities.Candidate
{
    public class Candidate : BaseEntitie<int, FullName>
    {
        public CandidatePhoto? PhotoUrl { get; set; }
        public int PoliticalPartyId { get; set; }
        public bool HasParticipatedInElection { get; set; }
    }
}
