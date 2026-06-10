using eVote360.Core.Domain.ValueObjects;
using eVote360.Core.Domain.Commom.BaseEntity;
using eVote360.Core.Domain.Common.Enums;

namespace eVote360.Core.Domain.Entities.User
{
    public class User : BaseEntitie<int, string>
    { 
        public required string UserFirstName { get; set; }

        public required string UserLastName { get; set; }

        public required UserEmail UserEmail { get; set; }
        public required UserPassword UserPassword { get; set; }

        public UserRole UserRole { get; set; }

        public required bool UserState { get; set; }   

        public User() { }
    }
}
