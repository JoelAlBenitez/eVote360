using eVote360.Core.Application.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.DTOs.Candidates
{
    public record CandidateDTO : BaseDto<int> 
    {

        public required string LastName { get; set; }
        public string? PhotoUrl { get; set; }
        public int PoliticalPartyId { get; set; }
        public bool HasParticipatedInElection { get; set; }


    }
}
