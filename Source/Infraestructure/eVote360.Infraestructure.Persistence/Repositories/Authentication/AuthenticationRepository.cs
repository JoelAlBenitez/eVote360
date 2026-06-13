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

        public async Task<bool> ExistUserAsync(string username, string password)
        {
            
              
             var result = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserFirstName == username);

            if (result == null) return false;
            // bool passworValid = BCrypt.Net.BCrypt.Verify(password, result.UserPassword);

            return true; //cambiar por el resultado de passworValid
           
        }

        public async Task<User> ReturnUserFindAsync(string username)
        {
            var result = ( await _context.Users
               .AsNoTracking()
               .FirstOrDefaultAsync(u => u.UserFirstName == username));

            return result!;
        }
    }
}
