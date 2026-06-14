using eVote360.Core.Application.DTOs.Login;
using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Contracts.Authentication.Query
{
    public interface ILoginQuery
    {
        Task<ValidationResult<LoginDto>> UserLoginCommand(LoginDto loginDto);

    }
}
