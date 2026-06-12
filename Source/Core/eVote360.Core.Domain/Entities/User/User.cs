using eVote360.Core.Domain.Commom.BaseEntity;
using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Settings.ValueObjects.UserEmail;
using eVote360.Core.Domain.Settings.ValueObjects.UserPassword;

namespace eVote360.Core.Domain.Entities.User
{
    public class User : BaseEntitie<int, string>
    { 
        public required string UserFirstName { get; set; }

        public required string UserLastName { get; set; }

        public required UserEmail UserEmail { get; set; }
        public required UserPassword UserPassword { get; set; }

        public UserRole UserRole { get; set; }
    }
}
