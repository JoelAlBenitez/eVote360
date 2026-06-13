using System.Collections.Generic;

namespace eVote360.Core.Application.ViewModels.PoliticalAlliances
{
    public class AllianceIndexViewModel
    {
        public List<AllianceViewModel> PendingReceived { get; set; } = new();
        public List<AllianceViewModel> SentRequests { get; set; } = new();
        public List<AllianceViewModel> ActiveAlliances { get; set; } = new();
        public bool HasActiveElection { get; set; }
    }
}
