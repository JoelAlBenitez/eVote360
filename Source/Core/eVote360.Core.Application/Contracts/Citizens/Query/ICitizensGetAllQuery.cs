using eVote360.Core.Application.DTOs.Citizens;

namespace eVote360.Core.Application.Contracts.Citizens.Query
{
    public interface ICitizensGetAllQuery
    {
        Task<IReadOnlyCollection<CitizensDto>> GetAllAsync();
    }
}
