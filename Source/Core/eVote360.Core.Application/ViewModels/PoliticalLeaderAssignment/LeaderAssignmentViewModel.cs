using eVote360.Core.Application.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.ViewModels.PoliticalLeaderAssignment
{
    public sealed class LeaderAssignmentViewModel : ViewModelBase<int>
    {
        public required int PoliticalLeaderId { get; set; }

        public required int PoliticalPartyId { get; set; }

        public required DateTime PoliticalAssignmentDate { get; set; }
    }
}
