using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Entities
{
    public class PoliticalAssignment
    {
        public int PoliticalAssignmentId { get; set; }

        public int PoliticalLeader { get; set; }

        public int PoliticalPartyId { get; set; }

        public DateTime PoliticalAssignmentDate { get; set; }

        public bool PoliticalAssignmentState = true;
    }
}
