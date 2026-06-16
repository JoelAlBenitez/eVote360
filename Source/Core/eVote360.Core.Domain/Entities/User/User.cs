using eVote360.Core.Domain.Commom.BaseEntity;
using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Settings.ValueObjects.Emails;
using eVote360.Core.Domain.Settings.ValueObjects.UserPassword;
using AssignmentEntities = eVote360.Core.Domain.Entities.PoliticalAssignment.PoliticalAssignment;

namespace eVote360.Core.Domain.Entities.User
{
    public class User

    { 
        public int Id { get; set; }

        public required string Name { get; set; }
        public required bool State {  get; set; }

        public required string UserFirstName { get; set; }

        public required string UserLastName { get; set; }

        public required Email UserEmail { get; set; }
        public required UserPassword UserPassword { get; set; }

        public UserRole UserRole { get; set; }

        //Navgation Property
        public virtual IReadOnlyCollection<AssignmentEntities> PoliticalAssignments { get; set; } = new List<AssignmentEntities>();


    }
}
