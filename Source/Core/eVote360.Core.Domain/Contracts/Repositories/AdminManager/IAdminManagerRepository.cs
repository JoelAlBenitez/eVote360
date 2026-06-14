namespace eVote360.Core.Domain.Contracts.Repositories.AdminManager
{
    public interface  IAdminManagerRepository
    {
        Task<int> CountCitizensRegisterAsync();
        Task<int> PoliticalPartyAsync();
        Task<int> CountElectionsRegisterAsync();
        Task<int> CountCandidactsRegisterAsync();
        Task<IReadOnlyCollection<int>> GetYears();
    }
}
