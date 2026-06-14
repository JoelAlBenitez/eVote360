using eVote360.Core.Application.Contracts.Users;
using eVote360.Core.Application.Contracts.Users.Commands;
using eVote360.Core.Application.DTOs.Users;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.UserRepository;
using eVote360.Core.Domain.Validators.UserValidator;
using eVote360.Core.Domain.Settings.ValueObjects;
using UserEntity = eVote360.Core.Domain.Entities.User.User;
using eVote360.Core.Domain.Settings.ValueObjects.Emails;
using eVote360.Core.Domain.Settings.ValueObjects.UserPassword;
using eVote360.Core.Application.Contracts.Authentication.Command;


namespace eVote360.Core.Application.Services.Users.CommandHandler
{
    public sealed class UserEdit : IUserEditCommand
    {
        private readonly IUserRepository _repository;
        private readonly IUserValidator _validator;
        private readonly IUserPasswordService _passwordService;
        private readonly ISessionUser _sessionUser;

        public UserEdit(IUserRepository userRepository, IUserValidator userValidator, IUserPasswordService passwordService, ISessionUser sessionUser)
        {
            _repository = userRepository;
            _validator = userValidator;
            _passwordService = passwordService;
            _sessionUser = sessionUser;
        }

        public async Task<ValidationResult> ExecuteAsync(UsersDto dto)
        {
            var errors = new List<Error>();

            try
            {
                if (dto.Id == null || dto.Id <= 0)
                {
                    errors.Add(new Error("USER EDIT ID", "El ID del usuarion es invalido para editar"));
                    return ValidationResult.Failure(errors);
                }

                string hashedPassword = _passwordService.HashPassword(dto.UserPassword);

                var user = new UserEntity
                {

                    Id = dto.Id,
                    CreateAt = dto.CreateAt,
                    CreateUserId = dto.CreateUserId,
                    State = dto.State,
                    UpdateAt = dto.UpdateAt,
                    UpdateUserId = _sessionUser.GetUserId(),

                    UserFirstName = dto.UserFirstName,
                    UserLastName = dto.UserLastName,
                    UserRole = dto.UserRole,
                    Name = dto.Name,

                    UserEmail = new Email(dto.UserEmail),
                    UserPassword = new UserPassword(hashedPassword)
                };

                var result = await _validator.ValidateUser(user, dto.UserPassword, _sessionUser.GetUserId());

                if (!result.IsValid)
                    return result;

                var isUpdated = await _repository.UpdateEntitieAsync(user);

                if (!isUpdated)
                {
                    errors.Add(new Error("USER UPDATE FAIL", "No se pudo actualizar la informacion del usuario"));
                    return ValidationResult.Failure(errors);
                }

                return ValidationResult.Success();
            }
            catch (ArgumentException ex) {
                errors.Add(new Error("USER VALIDATION ERROR", ex.Message));
                return ValidationResult.Failure(errors);
            }
        }

    }
}
