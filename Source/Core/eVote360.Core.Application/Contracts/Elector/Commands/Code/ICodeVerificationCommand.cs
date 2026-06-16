using eVote360.Core.Application.DTOs.Elector.Code;
using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Contracts.Elector.Commands.Code
{
    public  interface ICodeVerificationCommand
    {
        Task<ValidationResult> VerifyCodeVerification(CodeVerificationDto codeVerificationDto);
    }
}
