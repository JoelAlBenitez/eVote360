using eVote360.Core.Domain.Commom.BaseEntity;

using eVote360.Core.Domain.Entities.CandidateAssignment;
using eVote360.Core.Domain.Entities.Elector.Vote;

namespace eVote360.Core.Domain.Entities.ElectivePosition
{
    public class ElectivePositions : BaseEntitie<int, string>
    {
        public required string Description { get; set; }
     
        public ICollection<CandidateAssignment.CandidateAssignment>? CandidateAssignments { get; set; } 
        public ICollection<Votes>? votes { get; set; }
    }
}
