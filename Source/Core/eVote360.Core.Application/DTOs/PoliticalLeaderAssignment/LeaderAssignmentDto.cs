


﻿using eVote360.Core.Application.DTOs.Base;
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

        public required int PoliticalPartyId { get; set; }

        public required DateTimeOffset? CreateAt { get; set; }
        public required DateTimeOffset? UpdateAt { get; set; }

        public required DateTime PoliticalAssignmentDate { get; set; }

        public required int? CreateUserId { get; set; } 

        public required int? UpdateUserId { get; set; }

    }
}
