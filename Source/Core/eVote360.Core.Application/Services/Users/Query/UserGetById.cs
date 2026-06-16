using eVote360.Core.Application.Contracts.Users.Query;
using eVote360.Core.Application.DTOs.Users;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.UserRepository;


namespace eVote360.Core.Application.Services.Users.Query
{
    public sealed class UserGetById : IUserGetByIdQuery
    {
        private readonly IUserRepository _repository;

        public UserGetById(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult<UsersDto>> ExecuteAsync(int id)
        {
            var userEntity = await _repository.GetByIdEntitie(id);

            if (userEntity == null)
                return null!;

            var dto = new UsersDto
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                State = userEntity.State,

                UserFirstName = userEntity.UserFirstName,
                UserLastName = userEntity.UserLastName,
                UserRole = userEntity.UserRole,

                UserEmail = userEntity.UserEmail.Value,
                UserPassword = ""
            };
            return ValidationResult<UsersDto>.Success(dto);
        }
    }
}
