using eVote360.Core.Domain.Commom.BaseEntity;
using UserEntity = eVote360.Core.Domain.Entities.User.User;

namespace eVote360.Core.Domain.Entities.PoliticalAssignment
{
    public class PoliticalAssignment :BaseEntitie<int, string>
    {

        public required int PoliticalLeaderId { get; set; }

        public required int PoliticalPartyId { get; set; }

        public required DateTime PolitcalAssignmentDate {  get; set; }

        //NavigationProperty
        public virtual UserEntity PoliticalLeader { get; set; } = null!;
    }
}
