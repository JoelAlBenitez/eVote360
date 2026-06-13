using eVote360.Core.Domain.Common.Enums;
namespace eVote360.Core.Application.ViewModels.Login
{
    public sealed class LoggedUserViewModel
    {
        public required string UserName { get; set; }
        public required int IdUser { get; set; }
        public required UserRole userRole { get; set; }
    }
}
