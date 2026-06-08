using eVote360.Core.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Contracts.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task <User> GetEntityByIdAsync(int id);

        Task<IEnumerable<User>> GetAllAsync();

        Task<User> GetByUsernameAsync(string username);

        Task<User> GetByEmailAsync (string email);

        Task<int> CountActiveAdminAsync();


    }
}
