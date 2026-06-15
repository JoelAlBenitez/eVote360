using eVote360.Core.Domain.Commom.BaseEntity;
using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Settings.ValueObjects.ElectionDate;
using eVote360.Core.Domain.Entities.Elector.Vote;
using eVote360.Core.Domain.Entities.Elector.AuditVote;

namespace eVote360.Core.Domain.Entities.Election
{
    public class Election : BaseEntitie<int, string>
    {

        public required ElectionDate ElectionDate { get; set; }

        public required ElectionState ElectionState { get; set; }

        //Navigation Properties
        public virtual IReadOnlyCollection<Votes> Votes { get; set; } = new List<Votes>();
        public virtual IReadOnlyCollection<AuditVotes> AuditVotes{ get; set; } = new List<AuditVotes>();
    }
}
