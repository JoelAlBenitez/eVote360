using eVote360.Core.Application.Contracts.Authentication.Query;
using eVote360.Core.Application.DTOs.Login;
using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.AuthenticationAndAutorization;

namespace eVote360.Core.Application.Services.Authentication_Autorization.Query
{
    public class AuthenticationLoggedUserQuery : ILoginQuery
    {

        private readonly IAuthenticationRepository _authenticationRepository;
        private List<Error> _errors = new List<Error>();

        public AuthenticationLoggedUserQuery(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        public async Task<ValidationResult<LoginDto>> UserLoginCommand(LoginDto loginDto)
        {
            try
            {
                var user = await _authenticationRepository.ReturnUserFindAsync(loginDto.userName, loginDto.password);
                if (user == null)
                {
                    _errors.Add(AuthenticationAuthorizationError.DoesNotHaveEnoughPrivileges);
                    return ValidationResult<LoginDto>.Failure(_errors);
                }
                if (!user.state) _errors.Add(new Error("Usuario inactivo", "El usuario que ha ingresado se encuentra en un estado de inactividad, favor contactar con un administrador."));
                var dto = new LoginDto
                {
                    IdUser = user.IdUser,
                    userName = loginDto.userName,
                    password = loginDto.password,
                    Role = user.Role,
                    IdPoliticalParty = user.PoliticalPartyId
                };

                return ValidationResult<LoginDto>.Success(dto);
            }
            catch (Exception ex)
            {
                _errors.Add(new Error("Ha ocurrido un error inesperado", ex.Message));
                return ValidationResult<LoginDto>.Failure(_errors);
            }
        }
    }
}
