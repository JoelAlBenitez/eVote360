using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Common.CodeErrors;
using eVote360.Core.Domain.Common.Errors;
using Enums = eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Settings.ValueObjects.UserPassword;
using eVote360.Core.Domain.Contracts.ServiceValidates.User;

namespace eVote360.Core.Domain.Validators.UserValidator
{
    public class UserValidator : IUserValidator
    {
        private readonly IUserDomainService _userDomainService;

        public UserValidator(IUserDomainService userDomainService)
        {
            _userDomainService = userDomainService;
        }

        public async Task<ValidationResult> ValidateUser(Entities.User.User user, string plainPassword, int currentUserId) { 

            var errors = new List<Error>();

        var validations = new[]
        {

        await ValidateUsername(user.Name),
        await ValidateEmail(user.UserEmail.Value),
        await ValidateLastAdmin(user.Id, user.State == false),
        await ValidateNames(user.UserFirstName, user.UserLastName),
        await ValidateSelfDesactivation(user.Id,currentUserId, user.State == false),
        await ValidatePassword(plainPassword)
        };

        errors.AddRange(validations.Where(v => v != null));

            return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success();
        }


        private async Task<Error> ValidateUsername(string username)
        {
            var exists = await _userDomainService.ExistByUsernameAsync(username);
            if (exists) return UserErrors.UsernameAlreadyExist;
            return null!;
        }

        private async Task<Error> ValidateEmail(string email)
        {
            var exists = await _userDomainService.ExistByEmailAsync(email);
            if (exists) return UserErrors.EmailAlreadyExist;
            return null!;
        }
        private async Task<Error> ValidateLastAdmin(int userId, bool isDesactivating)
        {
            if (isDesactivating) { 
            var isLastAdmin = await _userDomainService.CountActiveAdminAsync();
            if (isLastAdmin <= 1) return UserErrors.LastAdminDesactivation;
            }
            return null!;
        }

        private async Task<Error> ValidateNames(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName)) return UserErrors.FirstNameRequired;
            if (string.IsNullOrWhiteSpace(lastName)) return UserErrors.LastNameRequired;
            return null!;
        }

        private async Task<Error> ValidateSelfDesactivation(int targetUserId, int currentUserId,bool isDesactivating)
        {
            if (isDesactivating && targetUserId == currentUserId)
            {
                return UserErrors.SelfDesactivation;
            }
            return null!;
        }

        private async Task<Error> ValidatePassword(string PlainPassword)
        {
            try
            {
                UserPassword.ValidateComplexity(PlainPassword);
                return null!;
            }
            catch (ArgumentException ex)
            {
                return new Error ("PasswordComplexity", ex.Message);
            }
        }
    }
}
