using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Contracts.ServiceValidates.User;
using eVote360.Core.Domain.Settings.ValueObjects.Emails;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eVote360.Infraestructure.Persistence.ServicesValidators.User
{
    public class UserServiceValidator : IUserDomainService
    {
        private readonly DbContextEVote360 _context;

        public UserServiceValidator(DbContextEVote360 context) { _context = context; }

            public async Task<int> CountActiveAdminAsync()
            {
                return await _context.Users
                    .AsNoTracking()
                    .CountAsync(x => x.UserRole == UserRole.Admin && x.State == true);
            }
   
            public async Task<bool> ExistByEmailAsync(string email)
            {
                return await _context.Users
                    .AsNoTracking()
                    .AnyAsync(x => x.UserEmail.Value == email);
            }

            public async Task<bool> ExistByUsernameAsync(string username)
            {
                return await _context.Users
                    .AsNoTracking()
                    .AnyAsync(x => x.Name == username);
            }

            public async Task<bool> ExistAnotherWithUsernameAsync(int userId, string username)
            {
                return await _context.Users
                    .AsNoTracking()
                    .AnyAsync(x => x.Name == username && x.Id != userId);
            }

            public async Task<bool> ExistAnotherWithEmailAsync(int userId, string email)
            {
                return await _context.Users
                    .AsNoTracking()
                    .AnyAsync(x => x.UserEmail.Value == email && x.Id != userId);
            }
    }
}
