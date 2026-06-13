using eVote360.Core.Application.DTOs.Login;
using eVote360.Core.Domain.Common.ValidationResult;
namespace eVote360.Core.Application.Contracts.Authentication.Command
{
    public interface ILoginCommand
    {
        Task<ValidationResult> AuthencationCommand(LoginDto loginDto); 
    }
}
