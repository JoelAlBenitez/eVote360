

namespace eVote360.Core.Domain.Contracts.DomainService.User
{
    public interface IUserDomainService
    {
        Task<int> CountActiveAdminAsync();

        Task<bool> ExistByEmailAsync(string email);

        Task<bool> ExistByUsernameAsync(string username);
    }
}
