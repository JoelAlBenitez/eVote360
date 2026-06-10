using eVote360.Core.Application.DTOs.ElectivePositions;
using eVote360.Core.Application.DTOs.Users;
using eVote360.Core.Domain.Common.ValidationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Contracts.Users
{
    public interface IUsersService
    {
        Task<ValidationResult> CreateAsync(UsersDto dto);
        Task<ValidationResult> UpdateAsync(UsersDto dto);
        Task<ValidationResult> AlterStateAsync(int id);
        Task <IEnumerable<UsersDto>> GetAllActiveUsersAsync();
        Task<IEnumerable<UsersDto>> GetAllAsync();
        Task<UsersDto> GetUserByIdAsync(int id);
    }
}
