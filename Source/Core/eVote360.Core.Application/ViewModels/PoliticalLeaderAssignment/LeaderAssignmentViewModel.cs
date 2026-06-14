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
        public string? PoliticalLeaderName { get; set; }
        public string? PoliticalLeaderUsername { get; set; }
        public bool PoliticalLeaderState { get; set; }

        public string? PoliticalPartyName { get; set; }
        public string? PoliticalPartyAcronym { get; set; }
        public bool PoliticalPartyState { get; set; }

        public required DateTime PoliticalAssignmentDate { get; set; }
    }
}
