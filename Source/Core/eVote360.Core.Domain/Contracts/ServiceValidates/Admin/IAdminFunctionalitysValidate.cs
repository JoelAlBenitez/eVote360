namespace eVote360.Core.Domain.Contracts.ServiceValidates.Admin
{
    public interface IAdminFunctionalitysValidate
    {
        Task<bool> ExistElectionByYear(int year);
        Task<bool> ExisteElections();
    }
}
