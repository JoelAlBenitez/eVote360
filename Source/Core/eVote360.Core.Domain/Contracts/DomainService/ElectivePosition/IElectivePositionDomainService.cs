namespace eVote360.Core.Domain.Contracts.DomainService.ElectivePosition
{
    public interface IElectivePositionDomainService
    {
        Task<bool> ExistElectivePositionByName(string Name);
        Task<bool> ElectivePositionUsedInElections(int Id, string Name);
        Task<bool> ElectivePositionHasAssociatedByCandidates(int Id, string Name );
        Task<bool> ExistElectivePositionByState(int Id, string Name, bool State);
    }
}
