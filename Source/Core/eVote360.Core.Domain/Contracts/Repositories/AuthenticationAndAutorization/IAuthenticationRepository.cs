using eVote360.Core.Domain.Entities.User;

namespace eVote360.Core.Domain.Contracts.Repositories.AuthenticationAndAutorization
{
    public interface IAuthenticationRepository
    {
        Task<User> ReturnUserFindAsync(string username, string password);
    }
}
