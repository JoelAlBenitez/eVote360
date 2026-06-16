using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.DTOs.Election
{
    public class ElectionResultDto
    {
        public string CandidateName { get; set; } = string.Empty;
        public string PartyName { get; set; } = string.Empty;
        public string PartyAcronym{ get; set; } = string.Empty;
        public string PartyLogo { get; set; } = string.Empty;
        public int TotalVotes{ get; set; } 
        public double Percentage{ get; set; } 
    }
}
