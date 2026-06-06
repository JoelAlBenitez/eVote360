using eVote360.Core.Domain.Commom.BaseEntity;
namespace eVote360.Core.Domain.Entities.ElectivePositions
{
    public class ElectivePositions : BaseEntitie<int, string>
    {
        public required string Description { get; set; }
        public required bool State {  get; set; }
    }
}
