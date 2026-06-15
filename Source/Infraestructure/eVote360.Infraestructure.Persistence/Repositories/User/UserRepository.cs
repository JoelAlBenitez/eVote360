using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Contracts.Repositories.UserRepository;
using eVote360.Core.Domain.Settings.ValueObjects.Emails;
using eVote360.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using UserEntitie = eVote360.Core.Domain.Entities.User.User;

namespace eVote360.Infraestructure.Persistence.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        protected readonly DbContextEVote360 _context;

        public UserRepository(DbContextEVote360 context) { _context = context; }

        public async Task<bool> CreateEntiteAsync(UserEntitie entite)
        {
            _context.Users.Add(entite);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<UserEntitie> GetByIdEntitie(int tkey)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == tkey);
        }

        public async Task<bool> UpdateEntitieAsync(UserEntitie entite)
        {
            _context.Update(entite);
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> AlterState(int tkey, bool state)
        {
            var user = await _context.Users.FindAsync(tkey);
              if (user == null) return false;
            
                user.State = state;
                  _context.Users.Update(user);
                  return await _context.SaveChangesAsync() > 0;
        }

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

            public async Task<IEnumerable<UserEntitie>> GetAllAsync()
            {
                return await _context.Users
                .AsNoTracking()
                .ToListAsync();
            }

         public async Task<IEnumerable<UserEntitie>> GetAllActivesAsync()
            {
                return await _context.Users
                .AsNoTracking()
                .Where(x => x.State == true)
                .ToListAsync();
             }

    }
}
