using eVote360.Core.Application.ViewModels.Base;
using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.ValueObjects;
using Enums = eVote360.Core.Domain.Common.Enums;

namespace eVote360.Core.Application.ViewModels.Users
{
    public sealed class UsersViewModel : ViewModelBase<int>
    {
        public required string UserFirstName { get; set; }

        public required string UserLastName { get; set; }

        public required string UserEmail { get; set; }

        public UserRole UserRole { get; set; }
    }
}
