using eVote360.Core.Domain.Commom.BaseEntity;
using eVote360.Core.Domain.Settings.ValueObjects.PoliticalPartyAcronym;
using AssignmentEntitie = eVote360.Core.Domain.Entities.PoliticalAssignment.PoliticalAssignment;
using CandidateEntitie = eVote360.Core.Domain.Entities.Candidate.Candidates;
using AllianceEntity = eVote360.Core.Domain.Entities.PoliticalAlliances.PoliticalAlliances;
using eVote360.Core.Domain.Settings.ValueObjects.Candidate;
using GenericFormatPhoto = eVote360.Core.Domain.Settings.ValueObjects.Candidate.CandidatePhoto;

namespace eVote360.Core.Domain.Entities.PoliticalParty
{
    public class PoliticalParty : BaseEntitie<int, string>
    {
        public required string PoliticalPartyDescription { get; set; }
        public required PoliticalPartyAcronym PoliticalPartyAcronym { get; set; }
        public required GenericFormatPhoto PoliticalPartyLogo { get; set; }

        //NavigationProperty
        public virtual IReadOnlyCollection<AssignmentEntitie> Assignments { get; set; } = null!;
        public virtual IReadOnlyCollection<CandidateEntitie> Candidates { get; set; } = null!;
        public virtual IReadOnlyCollection<AllianceEntity> RequestedAlliances{ get; set; } = null!;
        public virtual IReadOnlyCollection<AllianceEntity> ReceiveAlliances { get; set; } = null!;

    }
}
