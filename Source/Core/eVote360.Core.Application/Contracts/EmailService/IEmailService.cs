using eVote360.Core.Application.DTOs.Message;

namespace eVote360.Core.Application.Contracts.EmailService
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(MessageDto messageDto);
    }
}
