using eVote360.Core.Domain.Entities.User;

namespace eVote360.Core.Domain.Contracts.Repositories.AuthenticationAndAutorization
{
    public interface IAuthenticationRepository
    {
        Task<bool> ExistUserAsync(string username, string password);
        Task<User> ReturnUserFindAsync(string username);
    }
}
