using System;
using System.Collections.Generic;
using eVote360.Core.Domain.Common.ValidationResult;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Validators.UserValidator
{
    public interface IUserValidator
    {
        Task<ValidationResult> ValidateUser(Entities.User.User user, string plainPassword, int currentUserId = 0);
        Task<ValidationResult> ValidateAlterState(Entities.User.User user, int currentUserId = 0);
        Task<ValidationResult> ValidateUpdate(Entities.User.User user, string? plainPassword, int currentUserId = 0);
    }
}
