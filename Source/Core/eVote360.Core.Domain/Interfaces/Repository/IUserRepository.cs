using eVote360.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Interfaces.Repository
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
