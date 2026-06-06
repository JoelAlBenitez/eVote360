namespace eVote360.Core.Domain.DomainService.ElectivePositions
{
    public interface IElectivePositionDomainService
    {
        Task<bool> ExistElectivePositionByName(string name);
        Task<bool> ElectivePositionUsedInElections(int id, string name);

    }
}
