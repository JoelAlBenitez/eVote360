using eVote360.Core.Application.Contracts.Authentication.Command;
using eVote360.Core.Application.Contracts.Users.Commands;
using eVote360.Core.Application.DTOs.Users;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.UserRepository;
using eVote360.Core.Domain.Validators.UserValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Services.Users.CommandHandler
{
    public sealed class UserAlterState : IUserAlterStateCommand
    {
        private readonly IUserRepository _repository;
        private readonly IUserValidator _validator;
        public readonly ISessionUser _sessionUser;

        public UserAlterState(IUserRepository repository, IUserValidator validator, ISessionUser sessionUser)
        {
            _repository = repository;
            _validator = validator;
            _sessionUser = sessionUser;
        }

        public async Task<ValidationResult> ExecuteAsync(int id, bool state)
        {
                 var user = await _repository.GetByIdEntitie(id);
            
                if (user == null)
                {
                var errors = new List<Error> { new Error("USER ST", "No se encontró el usuario solicitado.") };
                return ValidationResult.Failure(errors);
                }
            
                if (state == false)
                {
                    user.State = false;
                    var validationResult = await _validator.ValidateAlterState(user, _sessionUser.GetUserId());
                    if (!validationResult.IsValid)
                    {
                        return validationResult;
                    }
                }

                var result = await _repository.AlterState(id, state);
            
                if (!result)
                {
                var errors = new List<Error> { new Error("USER ST", "No fue posible actualizar el estado en la base de datos.") };
                return ValidationResult.Failure(errors);
                }
            
                return ValidationResult.Success();
        }
    }
}
