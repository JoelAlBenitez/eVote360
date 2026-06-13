using eVote360.Core.Application.Contracts.Users.Query;
using eVote360.Core.Application.DTOs.Users;
using eVote360.Core.Domain.Contracts.Repositories.UserRepository;
using System.Linq;

namespace eVote360.Core.Application.Services.Users.Query
{
    public sealed class UserGetAll : IUserGetAllQuery
    {
        private readonly IUserRepository _userRepository;

        public UserGetAll(IUserRepository repository) { 
        
            _userRepository = repository;
        }

        public async Task<IReadOnlyCollection<UsersDto>> ExecuteAsync()
        {
            var entities = await _userRepository.GetAllAsync();
            return entities.Select(u => new UsersDto
            {
                Id = u.Id,
                Name = u.Name,
                State = u.State,

                CreateAt = u.CreateAt,
                UpdateAt = u.UpdateAt,
                CreateUserId = u.CreateUserId,
                UpdateUserId = u.UpdateUserId,
                UserFirstName = u.UserFirstName,
                UserLastName = u.UserLastName,
                UserRole = u.UserRole,

                UserEmail = u.UserEmail.Value,

                UserPassword = ""
            }).ToList();
        }
    }
}
