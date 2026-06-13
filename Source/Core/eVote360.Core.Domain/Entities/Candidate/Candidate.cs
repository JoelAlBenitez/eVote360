using eVote360.Core.Domain.Commom.BaseEntity;
using eVote360.Core.Domain.Settings.ValueObjects.CandidatePhoto;
using eVote360.Core.Domain.Settings.ValueObjects.FullName;

namespace eVote360.Core.Domain.Entities.Candidate
{
    public class Candidates : BaseEntitie<int, FullName>
    {
        public CandidatePhoto? PhotoUrl { get; set; }
        public int PoliticalPartyId { get; set; }
        public bool HasParticipatedInElection { get; set; }
    }
}
