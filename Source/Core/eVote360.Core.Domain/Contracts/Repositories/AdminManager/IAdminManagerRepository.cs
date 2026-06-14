using eVote360.Core.Domain.Entities.Admin;

namespace eVote360.Core.Domain.Contracts.Repositories.AdminManager
{
    public interface  IAdminManagerRepository
    {
        Task<int> CountCitizensRegisterAsync();
        Task<int> PoliticalPartyAsync();
        Task<int> CountElectionsRegisterAsync();
        Task<IReadOnlyCollection<Admin>> ElectionByYearAsync(DateTime year);
        Task<IReadOnlyCollection<int>> GetYears();
    }
}
