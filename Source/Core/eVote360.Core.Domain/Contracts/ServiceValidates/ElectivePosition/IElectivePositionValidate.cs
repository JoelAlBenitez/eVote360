namespace eVote360.Core.Domain.Contracts.DomainService.ElectivePosition
{
    public interface IElectivePositionValidate
    {
        Task<bool> ExistElectivePositionByName(string Name);
        Task<bool> ElectivePositionUsedInElections(int Id);
        Task<bool> ExistsAnotherElectivePositionWithName(int Id, string Name);
        Task<bool> ElectivePositionHasAssociatedByCandidates(int Id);
        Task<bool> ExistElectivePositionByState(int Id, string Name, bool State);
        Task<bool> ExistById(int Id);
        Task<bool> CurrentStateElectivePosiction(int Id);
    }
}
