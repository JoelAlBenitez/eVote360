using eVote360.Core.Application.Contracts.Users;
using BCrypt.Net;

namespace eVote360.Core.Application.Services.Users.UserPassword
{
    public class UserPassworrdService : IUserPasswordService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);    
        }

        public bool verifyPassword(string password, string hash) {
        return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
