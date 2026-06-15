using eVote360.Core.Domain.Entities.Elector.CodeVerifications;

namespace eVote360.Core.Domain.Contracts.Repositories.Elector.Otp
{
    public interface IOtpRepository
    {
        Task<bool> CreateAsync(CodeVerification codeVerification);
        Task<CodeVerification> GetByIdAndIdCitizens(Guid IdCitizens, int IdElection);
    }
}
