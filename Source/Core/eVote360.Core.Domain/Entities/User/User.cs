using eVote360.Core.Domain.Commom.BaseEntity;
using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Settings.ValueObjects.Emails;
using eVote360.Core.Domain.Settings.ValueObjects.Emails;
using eVote360.Core.Domain.Settings.ValueObjects.UserPassword;
using AssignmentEntitie = eVote360.Core.Domain.Entities.PoliticalAssignment.PoliticalAssignment;

namespace eVote360.Core.Domain.Entities.User
{
    public class User : BaseEntitie<int, string>
    { 
        public required string UserFirstName { get; set; }

        public required string UserLastName { get; set; }

        public required Email UserEmail { get; set; }
        public required UserPassword UserPassword { get; set; }

        public UserRole UserRole { get; set; }

        //Navgation Property
        public virtual IReadOnlyCollection<AssignmentEntitie> AssignmentEntitie { get; set; } = new List<AssignmentEntitie>();


    }
}
