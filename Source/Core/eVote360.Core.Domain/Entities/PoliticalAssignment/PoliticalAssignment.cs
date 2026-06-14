using eVote360.Core.Domain.Commom.BaseEntity;
using UserEntitie = eVote360.Core.Domain.Entities.User.User;
using PartyEntitie = eVote360.Core.Domain.Entities.PoliticalParty.PoliticalParty;

namespace eVote360.Core.Domain.Entities.PoliticalAssignment
{
    public class PoliticalAssignment :BaseEntitie<int, string>
    {

        public required int PoliticalLeaderId { get; set; }

        public required int PoliticalPartyId { get; set; }

        public required DateTime PolitcalAssignmentDate {  get; set; }


        //Navigation Property

        public virtual UserEntitie PoliticalLeader { get; set; } = null;
        public virtual PartyEntitie PoliticalParty { get; set; } = null;
    }
}
