using eVote360.Core.Domain.Commom.BaseEntity;
namespace eVote360.Core.Domain.Entities.ElectivePosition
{
    public class ElectivePositions : BaseEntitie<int, string>
    {
        public required string Description { get; set; }
     
    }
}
