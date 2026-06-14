using eVote360.Core.Application.DTOs.ElectivePositions;
using eVote360.Core.Application.DTOs.Users;
using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Contracts.Users.Query
{
    public interface IUserGetAllActivesQuery
    {
        Task<IReadOnlyCollection<UsersDto>> ExecuteAsync();
    }
}
