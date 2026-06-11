using eVote360.Core.Application.Contracts.Users.Query;
using eVote360.Core.Application.DTOs.Users;
using eVote360.Core.Domain.Contracts.Repositories.UserRepository;
using System.Linq;


namespace eVote360.Core.Application.Services.Users.Query
{
    public class UserGetAllActives : IUserGetAllActivesQuery
    {
        private readonly IUserRepository _repository;

        public UserGetAllActives(IUserRepository repository) {
        _repository = repository;
        }

        public async Task<IEnumerable<UsersDto>> ExecuteAsync()
        {
            var activeEntities = await _repository.GetAllActivesAsync();
            return activeEntities.Select(u => new UsersDto
            {
                Id = u.Id,
                Name = u.Name,
                State = u.State,

                CreateAt = u.CreateAt,
                UpdateAt = u.UpdateAt,
                CreateUserId = u.CreateUserId,
                UpdateUserId = u.UpdateUserId,

                Username = u.Name,
                UserFirstName = u.UserFirstName,
                UserLastName = u.UserLastName,
                UserRole = u.UserRole,

                UserEmail = u.UserEmail.Value,

                UserPassword = ""
            }).ToList();
        }
    }
}
