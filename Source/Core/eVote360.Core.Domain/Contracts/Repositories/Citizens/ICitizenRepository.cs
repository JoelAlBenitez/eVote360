using eVote360.Core.Domain.Contracts.Repositories.BaseRepository;
using eVote360.Core.Domain.Entities.Citizens;

namespace eVote360.Core.Domain.Contracts.Repositories.Citizens
{
    public interface ICitizenRepository : IBaseRepository<Citizen, Guid>
    {
        Task<IReadOnlyCollection<Citizen>> GetActiveCitizensByActive();
        Task<IReadOnlyCollection<Citizen>> GetAll();
        Task<Citizen> GetByIdentification(string Identification);

    }
}
