using eVote360.Core.Application.DTOs.PoliticalParty;
using eVote360.Core.Domain.Common.ValidationResult;

namespace eVote360.Core.Application.Contracts.PoliticalParty
{
    public interface IPoliticalPartyService
    {
        Task<ValidationResult> CreateAsync(PoliticalPartyDto dto);
        Task<ValidationResult> UpdateAsync(PoliticalPartyDto dto);
        Task<ValidationResult> AlterStateAsync(int id);
        Task<IEnumerable<PoliticalPartyDto>> GetAllAsync();
        Task<PoliticalPartyDto> GetByIdAsync(int id);
        Task<IEnumerable<PoliticalPartyDto>> GetActivePartiesAsync();
    }
}
