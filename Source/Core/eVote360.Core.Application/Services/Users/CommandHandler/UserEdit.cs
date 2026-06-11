using eVote360.Core.Application.Contracts.Users;
using eVote360.Core.Application.Contracts.Users.Commands;
using eVote360.Core.Application.DTOs.Users;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.UserRepository;
using eVote360.Core.Domain.Validators.UserValidator;
using eVote360.Core.Domain.ValueObjects;
using UserEntity = eVote360.Core.Domain.Entities.User.User;


namespace eVote360.Core.Application.Services.Users.CommandHandler
{
    public class UserEdit : IUserEditCommand
    {
        private readonly IUserRepository _repository;
        private readonly IUserValidator _validator;
        private readonly IUserPasswordService _passwordService;

        public UserEdit(IUserRepository userRepository, IUserValidator userValidator, IUserPasswordService passwordService)
        {
            _repository = userRepository;
            _validator = userValidator;
            _passwordService = passwordService;
        }

        public async Task<ValidationResult> ExecuteAsync(UsersDto dto)
        {
            string hashedPassword = _passwordService.HashPassword(dto.UserPassword);

            var user = new UserEntity
            {

                Id = dto.Id!,
                CreateAt = dto.CreateAt,
                CreateUserId = dto.CreateUserId,
                State = dto.State,
                UpdateAt = dto.UpdateAt,
                UpdateUserId =dto.UpdateUserId,

                UserFirstName = dto.UserFirstName,
                UserLastName = dto.UserLastName,
                UserRole = dto.UserRole,
                Name = dto.Name,

                UserEmail = new UserEmail(dto.UserEmail),
                UserPassword = new UserPassword(hashedPassword)
            };

            var result = await _validator.ValidateUser(user, dto.UserPassword, 1);

            if(!result.IsValid)
                return result;

            await _repository.UpdateEntitieAsync(user);

            return ValidationResult.Success();

        }

    }
}
