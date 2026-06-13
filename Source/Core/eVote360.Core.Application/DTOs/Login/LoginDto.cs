using eVote360.Core.Domain.Common.Enums;

namespace eVote360.Core.Application.DTOs.Login
{
    public sealed class LoginDto
    {

        public int IdUser { get; set; }
        public required string userName {  get; set; }
        public required string password { get; set; }
        public UserRole Role {  get; set; }
    }
}
