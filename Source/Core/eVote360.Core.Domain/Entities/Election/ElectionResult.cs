using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Domain.Entities.Election
{
    public class ElectionResult
    {
        public string CandidateNamer { get; set; } = string.Empty;
        public int TotalVotes { get; set; }
        public double Percentage { get; set; }
        public string PartyName { get; set; } = string.Empty;
        public string PartyAcronym { get; set; } = string.Empty;
        public string PartyLogo { get; set; } = string.Empty;
    }
}
