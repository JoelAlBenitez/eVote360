using eVote360.Core.Domain.Contracts.Repositories.BaseRepository;
using eVote360.Core.Domain.Entities.User;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserEntity = eVote360.Core.Domain.Entities.User.User;

namespace eVote360.Core.Domain.Contracts.Repositories.UserRepository
{
    public interface IUserRepository : IBaseRepository<Entities.User.User, int>
    {
        Task<int> CountActiveAdminAsync();

        Task<bool> ExistByEmailAsync(string email);

        Task<bool> ExistByUsernameAsync (string username);

        Task<IEnumerable<UserEntity>> GetAllAsync();
        Task<IEnumerable<UserEntity>> GetAllActivesAsync();


    }
}
