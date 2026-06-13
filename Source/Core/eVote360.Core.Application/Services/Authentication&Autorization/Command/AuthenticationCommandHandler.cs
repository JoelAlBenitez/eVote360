using eVote360.Core.Application.Contracts.Authentication.Command;
using eVote360.Core.Application.DTOs.Login;
using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.AuthenticationAndAutorization;

namespace eVote360.Core.Application.Services.Authentication_Autorization
{
    public class AuthenticationCommandHandler : ILoginCommand
    {

        private readonly IAuthenticationRepository _authenticationRepository;
        private List<Error> _errors = new List<Error>();

        public AuthenticationCommandHandler(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        public async Task<ValidationResult> AuthencationCommand(LoginDto loginDto)
        {
            try
            {
                var exist =  await _authenticationRepository.ExistUserAsync(loginDto.userName, loginDto.password);
                if (!exist) _errors.Add(AuthenticationAuthorizationError.UserNoFind);
                return _errors.Any() ? ValidationResult.Failure() : ValidationResult.Success();
            }
            catch (Exception ex) {

                _errors.Add(new Error("Ha ocurrido un error inesperado", ex.Message));
                return ValidationResult.Failure(_errors);
            }
        }

      
    }
}
