using eVote360.Core.Application.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.DTOs.PoliticalLeaderAssignment
{
    public record  LeaderAssignmentDto : BaseDto<int>
    {
        public required int PoliticalLeaderId { get; set; }
        public string? PoliticalLeaderName { get; set; }
        public string? PoliticalLeaderUsername { get; set; }
        public bool PoliticalLeaderState { get; set; }

        public required int PoliticalPartyId { get; set; }
        public string? PoliticalPartyName { get; set; }
        public string? PoliticalPartyAcronym { get; set; }
        public bool PoliticalPartyState { get; set; }

        public  DateTimeOffset? CreateAt { get; set; }
        public  DateTimeOffset? UpdateAt { get; set; }

        public required DateTime PoliticalAssignmentDate { get; set; }

        public  int? CreateUserId { get; set; } 

        public  int? UpdateUserId { get; set; }
    }
}
