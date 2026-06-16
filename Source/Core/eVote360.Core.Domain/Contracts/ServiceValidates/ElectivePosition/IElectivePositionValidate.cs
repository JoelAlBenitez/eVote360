namespace eVote360.Core.Domain.Contracts.DomainService.ElectivePosition
{
    public interface IElectivePositionValidate
    {
        Task<bool> ExistElectivePositionByName(string Name);
        Task<bool> ExistsAnotherElectivePositionWithName(int Id, string Name);
        Task<bool> ExistElectivePositionByState(int Id, string Name, bool State);
        Task<bool> ExistById(int Id);
        Task<bool> CurrentStateElectivePosiction(int Id);
    }
}
