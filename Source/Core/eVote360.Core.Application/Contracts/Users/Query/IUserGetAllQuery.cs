using eVote360.Core.Application.DTOs.Users;
using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Contracts.Users.Query
{
    public interface IUserGetAllQuery
    {
        Task<IEnumerable<UsersDto>> ExecuteAsync();
    }
}
