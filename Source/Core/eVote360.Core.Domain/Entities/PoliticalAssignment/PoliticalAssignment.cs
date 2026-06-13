using eVote360.Core.Domain.Commom.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Entities.PoliticalAssignment
{
    public class PoliticalAssignment :BaseEntitie<int, string>
    {

        public required int PoliticalLeaderId { get; set; }

        public required int PoliticalPartyId { get; set; }

        public required DateTime PolitcalAssignmentDate {  get; set; }
    }
}
