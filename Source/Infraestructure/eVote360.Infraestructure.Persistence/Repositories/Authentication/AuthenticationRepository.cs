using BCrypt.Net;
using eVote360.Core.Domain.Contracts.Repositories.AuthenticationAndAutorization;
using eVote360.Core.Domain.Entities.User;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
namespace eVote360.Infraestructure.Persistence.Repositories.Authentication
{
    public class AuthenticationRepository : IAuthenticationRepository
    {

        private readonly DbContextEVote360 _context;
        public AuthenticationRepository(DbContextEVote360 context) {
            _context = context;
        }

       
        public async Task<User> ReturnUserFindAsync(string username, string password)
        {
            var result = ( await _context.Users
               .AsNoTracking()
               .FirstOrDefaultAsync(u => u.UserFirstName == username));

            if (result != null) return null!;

            bool passwordValid = BCrypt.Net.BCrypt.Verify(password.ToString(), result!.UserPassword.HashValue);
            if (!passwordValid) return null!;

            return result;
        }
    }
}
