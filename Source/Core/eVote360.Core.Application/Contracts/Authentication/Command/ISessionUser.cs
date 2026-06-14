
using eVote360.Core.Domain.Common.Enums;

namespace eVote360.Core.Application.Contracts.Authentication.Command
{
    public interface ISessionUser
    {
        int GetUserId();
        UserRole GetRole();
        string GetUserName();

    }
}
