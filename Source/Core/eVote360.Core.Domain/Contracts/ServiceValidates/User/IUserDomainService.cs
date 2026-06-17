namespace eVote360.Core.Domain.Contracts.ServiceValidates.User
{
    public interface IUserDomainService
    {
        Task<int> CountActiveAdminAsync();

        Task<bool> ExistByEmailAsync(string email);

        Task<bool> ExistByUsernameAsync(string username);

        Task<bool> ExistAnotherWithUsernameAsync(int userId, string username);

        Task<bool> ExistAnotherWithEmailAsync(int userId, string email);
    }
}
